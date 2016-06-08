using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Common;
using System.Reflection;
using System.Dynamic;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace Data
{
    public class BizObject : DynamicObject, IEditableObject, INotifyPropertyChanged, IComparable<BizObject>
    {
        private readonly string _uniqueId;
        private BizObject _original;
        private bool _editing;
        private BizObjectDirtyFields _dirtyFields=new BizObjectDirtyFields();
        private BizObjectInfo _bizInfo;
        private bool _userHandle;
        private EntityStateType _entityState;
        private readonly Dictionary<string, object> _properties;
        private readonly bool _dynamicMode;

        [AttributeUsage(AttributeTargets.Property)]
        public class ColumnAttribute : Attribute
        {
            public ColumnAttribute()
            {
            }

            public ColumnAttribute(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        public void InitializeObject(BizObjectInfo bizInfo)
        {
            _bizInfo = bizInfo;
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }
        public T ConvertTo<T>() where T : BizObject, new()
        {
            if (GetType() == typeof(T)) return (T)this;

            var bo = new T();
            foreach(var property in GetDynamicMemberNames())
            {
                bo.SetFieldValue(property, GetFieldValue(property));
            }
            return bo;
        }
        [Bindable(false)]
        [Browsable(false)]
        [XmlIgnore]
        public bool Loading
        {
            get { return _userHandle; }
            set { _userHandle = value; }
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                result = _properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
        public object TryGetValue(string properyName)
        {
            if (_properties.ContainsKey(properyName))
            {
                return _properties[properyName];
            }
            else
            {
                return null;
            }
        }

        public bool TrySetValue(string propertyName, object value)
        {
            if (!_properties.ContainsKey(propertyName))
            {
                _properties.Add(propertyName, value);
            }
            else
            {
                _properties[propertyName] = value;
            }
            RaisePropertyChanged(propertyName);
            return true;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            
            return TrySetValue(binder.Name, value);
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class IgnoreAttribute : Attribute
        {
        }

        [Bindable(false)]
        [Browsable(false)]
        [XmlIgnore]
        public BizObjectDirtyFields DirtyFields
        {
            get { return _dirtyFields; }
            set { _dirtyFields = new BizObjectDirtyFields(value.Items); }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class TableNameAttribute : Attribute
        {
            public TableNameAttribute(string tableOwner, string tableName)
            {
                TableName = tableName;
                TableOwner = tableOwner;
            }

            public string TableName { get; private set; }
            public string TableOwner { get; private set; }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class PrimaryKeyAttribute : Attribute
        {
            public PrimaryKeyAttribute()
            {
                Sequence = string.Empty;
            }
            public PrimaryKeyAttribute(string sequence)
            {
                Sequence = sequence;
            }

            public string Sequence { get; private set; }
        }

        public enum EntityStateType
        {
            Unchanged,
            Added,
            Deleted,
            Modified
        }        
        
        [Bindable(false)]
        [Browsable(false)]
        public EntityStateType EntityState 
        { 
            get { return _entityState; }
            set { _entityState = value; RaiseUnboundPropertyChanged("EntityState"); }
        }

        public BizObject()
        {
            _editing = false;
            _uniqueId = Guid.NewGuid().ToString();
            EntityState = EntityStateType.Unchanged;
            _dynamicMode = false;
            _properties = new Dictionary<string, object>();
            if (this.GetType() == typeof(BizObject))
            {
                _dynamicMode = true;
            }
        }

        public BizObject(List<string> dynamicProperties, BizObjectInfo bizInfo=null) :this()
        {
            _dynamicMode = true;
            _properties = new Dictionary<string, object>(dynamicProperties.Count);
            foreach(var property in dynamicProperties)
            {
                _properties.Add(property, null);
            }
            _bizInfo = bizInfo;
        }

        public BizObject(Dictionary<string, object> objectProperties, BizObjectInfo bizInfo=null) : this()
        {
            _dynamicMode = true;
            _properties = objectProperties;
            _bizInfo = bizInfo;
        }


        public void EnabledUserHandle(bool userHandle = true)
        {
            _userHandle = userHandle;
        }

        public void ResetState()
        {
            EntityState = EntityStateType.Unchanged;
            _dirtyFields.Items.Clear();
        }

        private void SetDirtyField(string fieldName)
        {
            if (EntityState == EntityStateType.Unchanged)
            {
                EntityState = EntityStateType.Modified;
            }
            if (!_dirtyFields.Items.Contains(fieldName.ToUpper()))
            {
                _dirtyFields.Items.Add(fieldName.ToUpper());
            }
        }

        public void SetDirtyProperty(string propertyName)
        {
            SetDirtyField(Db.FieldPropertyNames[propertyName.ToUpper()]);
        }

        [XmlIgnore]
        [Bindable(false)]
        [Browsable(false)]
        public BizObjectInfo Db
        {
            get 
            {
                if (_bizInfo != null) return _bizInfo;
                _bizInfo = BizObjectInfoCache.GetOrBuild(this.GetType());
                if (_properties.Count == 0)
                {
                    foreach(var item in _bizInfo.FieldPropertyNames)
                    {
                        _properties.Add(item.Key, null);
                    }
                }
                return _bizInfo; 
            }

        }

        public object GetFieldValue(string fieldName)
        {
            if (_dynamicMode)
            {
                var val = _properties[fieldName];
                return val == DBNull.Value ? null : val;
            }
            else
            {
                var val= Db.FieldPropertyInfo[fieldName.ToUpper()].Getter(this);
                return val == DBNull.Value ? null : val;
            }
        }

        public void SetFieldValue(string fieldName, object value,bool raisePropertyChanged=false)
        {
            if (_dynamicMode)
            {
                _properties[fieldName] = value;
            }
            else
            {
                var field = Db.FieldPropertyInfo[fieldName];
                field.Setter(this, value);
            }
            if (raisePropertyChanged)
                RaiseUnboundPropertyChanged(fieldName);
        }

        public void SetFieldValue(string fieldName, object value, Reflection.GenericSetter setter, bool raisePropertyChanged = false)
        {
            if (_dynamicMode)
            {
                _properties[fieldName] = value;
            }
            else
            {
                setter(this, value);
            }
            if (raisePropertyChanged)
                RaiseUnboundPropertyChanged(fieldName);
        }

        [XmlIgnore]
        [Bindable(false)]
        [Browsable(false)]
        public string UniqueId
        {
            get { return _uniqueId; }
        }

        [XmlIgnore]
        [Bindable(false)]
        [Browsable(false)]
        public bool IsDirty
        {
            get { return EntityState != EntityStateType.Unchanged; }
        }

        [XmlIgnore]
        [Bindable(false)]
        [Browsable(false)]
        public bool IsNew
        {
            get { return EntityState == EntityStateType.Added; }
        }

        [XmlIgnore]
        [Bindable(false)]
        [Browsable(false)]
        public bool IsDeleted
        {
            get { return EntityState == EntityStateType.Deleted; }
            set
            {
                _editing = value;
                EntityState = EntityStateType.Deleted;
            }
        }

        public void BeginEdit()
        {
            if (!_editing)
            {
                _editing = true;
                _original = MemberwiseClone() as BizObject;
            }

        }

        public void EndEdit()
        {
            if (_editing)
            {
                _editing = false;
            }
        }

        public void CancelEdit()
        {
            _editing = false;
            RevertProperties();
            _original = null;
        }

        private void RevertProperties()
        {
            foreach (FieldPropertyInfo p in Db.FieldPropertyInfo.Values)
            {
                object propVal = p.Getter(_original);
                p.Setter(this, propVal);
            }
        }

        protected void RaiseUnboundPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        

        protected void RaisePropertyChanged(string name)
        {
            if (!_editing) return;
            if (_userHandle) return;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (_dynamicMode)
            {
                SetDirtyField(name);
            }
            else
            {
                SetDirtyField(Db.FieldPropertyNames[name.ToUpper()]);
            }
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static DbType ParseValueType(Type t)
        {
            string typeName = t.Name.ToUpper();
            if (typeName.StartsWith("NULLABLE", StringComparison.Ordinal))
            {
                typeName = t.FullName.ToUpper();
            }
            if (typeName.Contains("STRING"))
            {
                return DbType.String;
            }
            if (typeName.Contains("INT"))
            {
                if (typeName.Contains("16"))
                    return DbType.Int16;
                if (typeName.Contains("32"))
                    return DbType.Int32;
                if (typeName.Contains("64"))
                    return DbType.Int64;
                return DbType.Int32;
            }
            if (typeName.Contains("DATE"))
            {
                return DbType.DateTime;
            }
            if (typeName.Contains("TIME"))
            {
                return DbType.DateTime;
            }
            if (typeName.Contains("DECIMAL"))
            {
                return DbType.Decimal;
            }
            return DbType.String;
        }


        
        public string Serialize()
        {
            try
            {
                var ser = new XmlSerializer(GetType());
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    ser.Serialize(sw, this);
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object Deserialize(Type type, string data)
        {
            try
            {
                object objData;
                var ser = new XmlSerializer(type);
                using (var sw = new StringReader(data))
                {
                    objData = ser.Deserialize(sw);
                }
                return objData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int CompareTo(BizObject other)
        {
            bool isEqual=true;
            foreach (var field in Db.Fields)
            {
                if (!GetFieldValue(field).Equals(other.GetFieldValue(field)))
                {
                    isEqual = false;
                    break;
                }
            }

            if (isEqual) return 0;
            if (Db.PrimaryKeys != null && Db.PrimaryKeys.Count > 0)
            {
                object pkA = GetFieldValue(Db.PrimaryKeys[0]);
                object pkB = other.GetFieldValue(Db.PrimaryKeys[0]);
                if (pkA == null && pkB != null) return -1;
                if (pkA != null && pkB == null) return 1;
                if (pkA == null) return 2;
                return String.Compare(pkA.ToString(), pkB.ToString(), StringComparison.OrdinalIgnoreCase);
            }
            return -2;
        }
        #region "Property Setters"
        protected void SetPropertyValue(string propertyName, ref decimal? curValue, decimal? newValue)
        {
            if (curValue != newValue)
            {

                if (!_dynamicMode)
                {
                    curValue = newValue;
                    RaisePropertyChanged(propertyName);
                }
                else
                {
                    TrySetValue(propertyName, newValue);
                }

            }
        }
        protected void SetPropertyValue(string propertyName, ref int? curValue, int? newValue)
        {
            if (curValue != newValue)
            {

                if (!_dynamicMode)
                {
                    curValue = newValue;
                    RaisePropertyChanged(propertyName);
                }
                else
                {
                    TrySetValue(propertyName, newValue);
                }

            }
        }
        protected void SetPropertyValue(string propertyName, ref DateTime? curValue, DateTime? newValue)
        {
            if (curValue != newValue)
            {

                if (!_dynamicMode)
                {
                    curValue = newValue;
                    RaisePropertyChanged(propertyName);
                }
                else
                {
                    TrySetValue(propertyName, newValue);
                }

            }
        }
        protected void SetPropertyValue(string propertyName, ref string curValue, string newValue)
        {
            if (curValue != newValue)
            {

                if (!_dynamicMode)
                {
                    curValue = newValue;
                    RaisePropertyChanged(propertyName);
                }
                else
                {
                    TrySetValue(propertyName, newValue);
                }

            }
        }
        #endregion 
    }

    public class BizObjectInfo
    {
        public Type Type { get; internal set; }
        public string TableName { get; set; }
        public string TableOwner { get; set; }
        public List<string> Fields { get; set; }
        public List<string> PrimaryKeys { get; set; }
        public Dictionary<string,string> Sequences { get; set; }
        public Dictionary<string, string> FieldPropertyNames { get; set; }
        public Dictionary<string, FieldPropertyInfo> FieldPropertyInfo { get; set; }

        public BizObjectInfo(Type t)
        {
            this.Type = t;
            this.Fields = new List<string>();
            this.PrimaryKeys = new List<string>();
            this.Sequences = new Dictionary<string, string>();
            this.FieldPropertyInfo = new Dictionary<string, FieldPropertyInfo>();
            this.FieldPropertyNames = new Dictionary<string, string>();

            ReadBizData(t);
        }

        
        public void ReadBizData(Type t)
        {
            foreach (System.Reflection.PropertyInfo p in t.GetProperties())
            {
                if (p.CanWrite && p.CanRead)
                {
                    var ignoreAttr = p.GetCustomAttributes(typeof(Data.BizObject.IgnoreAttribute), true);
                    if (ignoreAttr.Length == 0)
                    {
                        var columnNameAttr = p.GetCustomAttributes(typeof(Data.BizObject.ColumnAttribute), true);
                        if (columnNameAttr.Length > 0)
                        {
                            string fieldName = ((Data.BizObject.ColumnAttribute)columnNameAttr[0]).Name.ToUpper();
                            Fields.Add(fieldName);
                            var targetType = p.PropertyType.IsNullableType()
                                            ? Nullable.GetUnderlyingType(p.PropertyType)
                                            : p.PropertyType;

                            var fieldInfo = new FieldPropertyInfo
                            {
                                Type = p.PropertyType,
                                Name = p.Name,
                                DbFieldType = BizObject.ParseValueType(p.PropertyType),
                                Setter = Reflection.CreateSetMethod(p),
                                Getter = Reflection.CreateGetMethod(p)
                            };
                            FieldPropertyNames.Add(p.Name.ToUpper(), fieldName.ToUpper());
                            FieldPropertyInfo.Add(fieldName.ToUpper(), fieldInfo);

                            var pkAttr = p.GetCustomAttributes(typeof(Data.BizObject.PrimaryKeyAttribute), true);
                            if (pkAttr.Length > 0)
                            {
                                PrimaryKeys.Add(fieldName.ToUpper());
                                var pkObj=pkAttr[0] as Data.BizObject.PrimaryKeyAttribute;
                                if (pkObj != null)
                                {
                                    string sequence = pkObj.Sequence;
                                    if (!string.IsNullOrEmpty(sequence))
                                        Sequences.Add(fieldName.ToUpper(), sequence);
                                }
                            }
                        }
                    }
                }
            }
            var a = t.GetCustomAttributes(typeof(Data.BizObject.TableNameAttribute), true);
            if (a.Length > 0)
            {
                TableName = ((Data.BizObject.TableNameAttribute)a[0]).TableName;
                TableOwner = ((Data.BizObject.TableNameAttribute)a[0]).TableOwner;
            }
            BizObjectInfoCache.Add(t, this);
        }
        public static FieldPropertyInfo ParseDbField(string typeName)
        {
            //ToDo: make function ....
            var pi = new FieldPropertyInfo();
            if (typeName.ToUpper().Contains("CHAR") || typeName.ToUpper().Contains("CLOB"))
            {
                pi.DbFieldType = DbType.String;
                pi.Type = typeof(string);
            }
            else if (typeName.ToUpper().Contains("NUM"))
            {
                pi.DbFieldType = DbType.VarNumeric;
                pi.Type = typeof(decimal?);
            }
            else if (typeName.ToUpper().Contains("DATE") || typeName.ToUpper().Contains("TIMESTAMP"))
            {
                pi.DbFieldType = DbType.DateTime;
                pi.Type = typeof(DateTime?);
            }
            else
            {
                pi.DbFieldType = DbType.String;
                pi.Type = typeof(string);
            }
            return pi;
        }
    }
    public class BizPropertyInfo
    {
        
        public BizPropertyInfo() { }
        public BizPropertyInfo(string name, DbType dbType, Type type)
        {
            Name = name;
            DbFieldType = dbType;
            Type = type;
        }
        public DbType DbFieldType { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
    }
    
    public class FieldPropertyInfo: BizPropertyInfo
    {
        public Reflection.GenericGetter Getter;
        public Reflection.GenericSetter Setter;
    }

    public class BizProperty
    {
        public string PropertyName { get; set; }
        public int ReaderIndex { get; set; }
        public FieldPropertyInfo PropertyInfo { get; set; }
        public Type ConverterType { get; set; }
    }
}
