using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public static class DbContext
    {
        private static string paramStr = "@";
        public static string RowId = "ROWID";
        #region "Query Functions and Utils"
        private static void FieldsCheck(BizObjectInfo bi, List<string> selectFields)
        {
            foreach (var key in selectFields)
            {
                if (!bi.FieldPropertyInfo.Keys.Contains(key)) throw new Exception("Field: " + key + " does not exists in object [" + bi.Type.Name + "]!!");
            }
        }

        
        
        public static T QuerySingle<T>(this IDbConnection cnn) where T : BizObject, new()
        {
            return QuerySingle<T>(cnn, string.Empty, new List<string>());
        }

        public static T QuerySingle<T>(this IDbConnection cnn, string condition) where T : BizObject, new()
        {
            return QuerySingle<T>(cnn, condition, new List<string>());
        }

        public static T QuerySingle<T>(this IDbConnection cnn, string condition, string selectFields) where T : BizObject, new()
        {
            if (string.IsNullOrEmpty(selectFields))
            {
                return QuerySingle<T>(cnn, condition, new List<string>());
            }
            var fields =
                new List<string>(selectFields.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            if (fields.Count > 0)
            {
                return QuerySingle<T>(cnn, condition, fields);
            }
            return QuerySingle<T>(cnn, condition, new List<string>());
        }

        public static T QuerySingle<T>(this IDbConnection cnn, string condition, List<string> selectFields = null) where T : BizObject, new()
        {
            var bi = BizObjectInfoCache.GetOrBuild(typeof(T));
            string sql = GetQueryString(bi, selectFields, condition);

            if (!String.IsNullOrEmpty(sql))
            {
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        var item = new T();
                        if (reader != null && reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (!reader.IsDBNull(i))
                                {
                                    var val = reader.GetValue(i);
                                    var name = reader.GetName(i);
                                    item.SetFieldValue(name, Convert.ChangeType(val, item.Db.FieldPropertyInfo[name].Type));
                                }
                            }
                            return item;
                        }
                    }
                }
            }
            return null;
        }

        public static IEnumerable<T> Query<T>(this IDbConnection cnn) where T : BizObject, new()
        {
            return Query<T>(cnn, string.Empty, new List<string>());
        }

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string condition)
            where T : BizObject, new()
        {
            return Query<T>(cnn, condition, new List<string>());
        }

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string condition, string selectFields) where T : BizObject, new()
        {
            if (string.IsNullOrEmpty(selectFields))
            {
                return Query<T>(cnn, condition, new List<string>());
            }
            var fields =
                new List<string>(selectFields.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            if (fields.Count > 0)
            {
                return Query<T>(cnn, condition, fields);
            }
            return Query<T>(cnn, condition, new List<string>());
        }
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string condition, List<string> selectFields = null, CommandBehavior cmdBehavior = CommandBehavior.Default) where T : BizObject, new()
        {
            var bi = BizObjectInfoCache.GetOrBuild(typeof(T));
            string sql = GetQueryString(bi, selectFields, condition);

            if (String.IsNullOrEmpty(sql))
            {
                yield return null;
            }
            else
            {
                var sw = new Stopwatch();
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = cmd.ExecuteReader(cmdBehavior))
                    {
                        if (reader == null) yield return default(T);

                        var tmp = new T();
                        var boi = tmp.Db;
                        var properties = new List<ReaderFieldProperties>();
                        var objProps = new Dictionary<string, FieldPropertyInfo>();

                        foreach (KeyValuePair<string, FieldPropertyInfo> field in boi.FieldPropertyInfo)
                        {
                            objProps.Add(field.Key.ToLower(), field.Value);
                        }
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i).ToLower();
                            if (objProps.ContainsKey(name))
                            {
                                properties.Add(new ReaderFieldProperties(i,name, objProps[name]));
                            }
                        }
                        while (reader.Read())
                        {
                            var item = new T();
                            item.Loading = true;
                            foreach(var property in properties)
                            {
                                property.PropertyInfo.Setter(item, reader.GetValue(property.ColumnIndex));
                            }
                            item.Loading = false;
                            yield return item;
                        }
                    }
                }
            }
        }

        public async static Task<IList<T>> QueryAsync<T>(this IDbConnection cnn) where T : BizObject, new()
        {
            return await QueryAsync<T>(cnn, string.Empty, new List<string>());
        }

        public async static Task<IList<T>> QueryAsync<T>(this IDbConnection cnn, string condition)
            where T : BizObject, new()
        {
            return await QueryAsync<T>(cnn, condition, new List<string>());
        }

        public async static Task<IList<T>> QueryAsync<T>(this IDbConnection cnn, string condition, string selectFields) where T : BizObject, new()
        {
            if (string.IsNullOrEmpty(selectFields))
            {
                return await QueryAsync<T>(cnn, condition, new List<string>());
            }
            var fields =
                new List<string>(selectFields.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            if (fields.Count > 0)
            {
                return await QueryAsync<T>(cnn, condition, fields);
            }
            return await QueryAsync<T>(cnn, condition, new List<string>());
        }
        public async static Task<IList<T>> QueryAsync<T>(this IDbConnection cnn, string condition, List<string> selectFields = null, CommandBehavior cmdBehavior = CommandBehavior.Default) where T : BizObject, new()
        {
            var bi = BizObjectInfoCache.GetOrBuild(typeof(T));
            string sql = GetQueryString(bi, selectFields, condition);

            if (String.IsNullOrEmpty(sql))
            {
                return new List<T>();
            }
            else
            {
                var sw = new Stopwatch();
                using (DbCommand cmd = cnn.CreateCommand() as DbCommand)
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = await cmd.ExecuteReaderAsync(cmdBehavior))
                    {
                        return await Task.Run<IList<T>>(() => {

                            if (reader == null) return new List<T>{ default(T) };

                            var tmp = new T();
                            var boi = tmp.Db;
                            var properties = new List<ReaderFieldProperties>();
                            var objProps = new Dictionary<string, KeyValuePair<string, FieldPropertyInfo>>();

                            foreach (KeyValuePair<string, FieldPropertyInfo> field in boi.FieldPropertyInfo)
                            {
                                objProps.Add(field.Key.ToLower(), field);
                            }
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i).ToLower();
                                if (objProps.ContainsKey(name))
                                {
                                    properties.Add(new ReaderFieldProperties(i,name, objProps[name].Value));
                                }
                            }
                            var createBizInstance = BizReflection.BizInstanceBuilder<T>(reader, properties);

                            var _items = new List<T>();
                            while (reader.Read())
                            {

                                _items.Add(createBizInstance(reader));
                            }
                            return _items;
                        });
                    }
                }
            }
        }

        public static BizObjectBindingList<T> QueryDataBinding<T>(this IDbConnection cnn)
            where T : BizObject, new()
        {
            return QueryDataBinding<T>(cnn, string.Empty);
        }

        public static BizObjectBindingList<T> QueryDataBinding<T>(this IDbConnection cnn, string condition, List<string> selectFields = null,
                                                               CommandBehavior cmdBehavior = CommandBehavior.Default)
            where T : BizObject, new()
        {
            var bl = new BizObjectBindingList<T>(Query<T>(cnn, condition, selectFields, cmdBehavior));
            return bl;
        }
        public async static Task<BizObjectBindingList<T>> QueryDataBindingAsync<T>(this IDbConnection cnn)
            where T : BizObject, new()
        {
            return await QueryDataBindingAsync<T>(cnn, string.Empty);
        }

        public async static Task<BizObjectBindingList<T>> QueryDataBindingAsync<T>(this IDbConnection cnn, string condition, List<string> selectFields = null,
                                                               CommandBehavior cmdBehavior = CommandBehavior.Default)
            where T : BizObject, new()
        {
            var items = await QueryAsync<T>(cnn, condition, selectFields, cmdBehavior);
            return await Task.Run<BizObjectBindingList<T>>(() =>
            {
                var bl = new BizObjectBindingList<T>(items);
                return bl;
            });
        }

        public async static Task<BizObjectBindingList<BizObject>> QueryDataBindingDynamicAsync(this IDbConnection cnn, string query, BizObjectInfo bi=null,
                                                               CommandBehavior cmdBehavior = CommandBehavior.Default)
        {
            var items = await QueryDynamicAsync(cnn, query, bi,cmdBehavior);
            return await Task.Run<BizObjectBindingList<BizObject>>(() =>
            {
                var bl = new BizObjectBindingList<BizObject>(items);
                return bl;
            });
        }


        #endregion
        public static string GetQueryString(BizObjectInfo bi, List<string> selectFields = null, string condition = null)
        {
            string sql;
            var fields = "";

            if (bi.Type.IsSubclassOf(typeof(BizObjectView)))
            {
                var view = Activator.CreateInstance(bi.Type) as BizObjectView;
                object sqlString = view.GetView(condition);
                if (sqlString != null)
                {
                    if (selectFields == null || selectFields.Count == 0)
                    {
                        sql = sqlString.ToString();
                    }
                    else
                    {
                        FieldsCheck(bi, selectFields);

                        fields = selectFields.Aggregate(fields,
                                                        (current, selectField) => current + (selectField + ", "));
                        fields = fields.Length > 2 ? fields.Substring(0, fields.Length - 2) : "*";
                        sql = "SELECT " + fields + " FROM (" + sqlString + ")";
                    }

                }
                else
                {
                    throw new Exception("Type: " + bi.Type.Name + " GetView returns null or empty string!!!");
                }
            }
            else
            {

                if (condition.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    sql = condition;
                }
                else
                {

                    if (string.IsNullOrEmpty(bi.TableName)) throw new Exception("Type: " + bi.Type.Name + " has no table name!!!");
                    string table = bi.TableName;

                    if (selectFields == null || selectFields.Count == 0)
                    {
                        fields = bi.FieldPropertyInfo.Aggregate(fields, (current, field) => current + (field.Key + ", "));
                        fields = fields.Length > 2 ? fields.Substring(0, fields.Length - 2) : "*";

                        if (bi.TableOwner != "")
                        {
                            table = bi.TableOwner + "." + table;
                        }
                    }
                    else
                    {
                        FieldsCheck(bi, selectFields);

                        fields = selectFields.Aggregate(fields, (current, selectField) => current + (selectField + ", "));
                        fields = fields.Length > 2 ? fields.Substring(0, fields.Length - 2) : "*";
                    }

                    sql = "SELECT " + fields + " FROM " + table;
                    if (!string.IsNullOrEmpty(condition))
                    {
                        sql += " WHERE " + condition;
                    }
                }
            }
            return sql;
        }

        public static string GetQueryString(string owner, string table, string condition = null, IEnumerable<string> fields = null)
        {
            string sql = "";
            string tableFields = table + ".* ";
            bool first = true;
            if (fields != null)
            {
                tableFields = "";
                foreach (var field in fields)
                {
                    if (first)
                    {
                        tableFields += table + "." + field;
                        first = false;
                    }
                    else
                    {
                        tableFields += ", " + table + "." + field;
                    }

                }
            }
            if (string.IsNullOrEmpty(owner))
            {
                sql = "SELECT " + table + "." + RowId + ", " + tableFields + " FROM " + table;
            }
            else
            {
                sql = "SELECT " + table + "." + RowId + ", " + tableFields + " FROM " + owner + "." + table;
            }
            if (!string.IsNullOrEmpty(condition))
            {
                sql += " WHERE " + condition;
            }
            return sql;
        }

        public static IEnumerable<BizObject> QueryDynamic(this IDbConnection cnn,string query,BizObjectInfo bizInfo=null, CommandBehavior cmdBehavior = CommandBehavior.Default)
        {
            string sql = query;

            if (String.IsNullOrEmpty(sql))
            {
                yield return null;
            }
            else
            {
                var sw = new Stopwatch();
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = cmd.ExecuteReader(cmdBehavior))
                    {
                        if (reader == null) yield return null;
                        var properties = new Dictionary<string, object>(reader.FieldCount);

                        while (reader.Read())
                        {
                            var itemData = new Dictionary<string, object>(reader.FieldCount);
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                itemData.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            yield return new BizObject(itemData,bizInfo);
                        }
                    }
                }
            }
        }

        public async static Task<IList<BizObject>> QueryDynamicAsync(this IDbConnection cnn, string query, BizObjectInfo bizInfo = null, CommandBehavior cmdBehavior = CommandBehavior.Default)
        {
            string sql = query;

            if (String.IsNullOrEmpty(sql))
            {
                return new List<BizObject>();
            }
            else
            {
                var sw = new Stopwatch();
                using (DbCommand cmd = cnn.CreateCommand() as DbCommand)
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = await cmd.ExecuteReaderAsync(cmdBehavior))
                    {
                        if (reader == null) return new List<BizObject>();

                        return await Task.Run(() =>
                        {
                            var _items = new List<BizObject>();
                            var properties = new Dictionary<string, object>(reader.FieldCount);
                            while (reader.Read())
                            {
                                var itemData = new Dictionary<string, object>(reader.FieldCount);
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    itemData.Add(reader.GetName(i), reader.GetValue(i));
                                }

                                _items.Add(new BizObject(itemData, bizInfo));
                            }
                            return _items;
                        });
                    }
                }
            }
        }
        public async static Task<IList<T>> QueryDynamicAsync<T>(this IDbConnection cnn, string query, CommandBehavior cmdBehavior = CommandBehavior.Default) where T : BizObject, new()
        {
            var bi = BizObjectInfoCache.GetOrBuild(typeof(T));
            string sql = query;

            if (String.IsNullOrEmpty(sql))
            {
                return new List<T>();
            }
            else
            {
                var tmp = new T();
                var boi = tmp.Db;
                var properties = new List<ReaderFieldProperties>();
                var objProps = new Dictionary<string, KeyValuePair<string, FieldPropertyInfo>>();

                using (DbCommand cmd = cnn.CreateCommand() as DbCommand)
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = await cmd.ExecuteReaderAsync(cmdBehavior))
                    {
                        if (reader == null) return new List<T>();
                        return await Task.Run(() =>
                        {
                            var _items = new List<T>();
                            foreach (KeyValuePair<string, FieldPropertyInfo> field in boi.FieldPropertyInfo)
                            {
                                objProps.Add(field.Key.ToLower(), field);
                            }
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i).ToLower();
                                if (objProps.ContainsKey(name))
                                {
                                    properties.Add(new ReaderFieldProperties(i,name,objProps[name].Value));
                                }
                            }
                            var createBizInstance = BizReflection.BizInstanceBuilder<T>(reader, properties);
                            while (reader.Read())
                            {
                                _items.Add(createBizInstance(reader));
                            }
                            return _items;
                        });
                    }
                }
            }
        }
        public static IEnumerable<T> QueryDynamic<T>(this IDbConnection cnn, string query, CommandBehavior cmdBehavior = CommandBehavior.Default) where T : BizObject, new()
        {
            var bi = BizObjectInfoCache.GetOrBuild(typeof(T));
            string sql = query;

            if (String.IsNullOrEmpty(sql))
            {
                yield return null;
            }
            else
            {
                var tmp = new T();
                var boi = tmp.Db;
                var properties = new List<ReaderFieldProperties>();
                var objProps = new Dictionary<string, KeyValuePair<string, FieldPropertyInfo>>();

                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 5000;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    using (IDataReader reader = cmd.ExecuteReader(cmdBehavior))
                    {
                        if (reader == null) yield return default(T);

                        foreach (KeyValuePair<string, FieldPropertyInfo> field in boi.FieldPropertyInfo)
                        {
                            objProps.Add(field.Key.ToLower(), field);
                        }
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i).ToLower();
                            if (objProps.ContainsKey(name))
                            {
                                properties.Add(new ReaderFieldProperties(i, name, objProps[name].Value));
                            }
                        }
                        var createBizInstance = BizReflection.BizInstanceBuilder<T>(reader, properties);
                        while (reader.Read())
                        {
                            yield return createBizInstance(reader);
                        }
                    }
                }
            }
        }

        public static BizObject QuerySingleDynamic(this IDbConnection cnn, string query, CommandBehavior cmdBehavior = CommandBehavior.Default)
        {
            return QuerySingleDynamic<BizObject>(cnn, query, cmdBehavior);
        }

        public static T QuerySingleDynamic<T>(this IDbConnection cnn, string query, CommandBehavior cmdBehavior = CommandBehavior.Default) where T : BizObject, new()
        {
            string sql = query;

            if (String.IsNullOrEmpty(sql))
            {
                return null;
            }
            else
            {
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    cmd.CommandTimeout = 5000;
                    using (IDataReader reader = cmd.ExecuteReader(cmdBehavior | CommandBehavior.SingleRow))
                    {
                        if (reader != null && reader.Read())
                        {
                            var itemData = new Dictionary<string, object>(reader.FieldCount);
                            for(var i = 0; i < reader.FieldCount; i++)
                            {
                                itemData.Add(reader.GetName(i), reader[i]);
                            }
                            return new BizObject(itemData).ConvertTo<T>();
                        }
                    }
                }
            }
            return null;
        }

        public static int Insert<T>(this IDbConnection cnn, T bo) where T : BizObject
        {
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();

                cnn.FillPrimaryKeys(bo);
                string tableName = bo.Db.TableName;
                if (!string.IsNullOrEmpty(bo.Db.TableOwner))
                {
                    tableName = bo.Db.TableOwner + "." + tableName;
                }
                string sql = "INSERT INTO " + tableName + "(";
                using (IDbCommand cmd = cnn.CreateCommand())
                {

                    foreach (var field in bo.Db.FieldPropertyInfo)
                    {
                        sql += field.Key + ", ";
                    }
                    if (sql.EndsWith(", ", StringComparison.OrdinalIgnoreCase))
                    {
                        sql = sql.Remove(sql.Length - 2);
                    }
                    sql += ") VALUES (";

                    foreach (var field in bo.Db.FieldPropertyInfo)
                    {
                        IDbDataParameter cmdParam = cmd.CreateParameter();
                        sql += paramStr + field.Key + ", ";
                        object paramValue = field.Value.Getter(bo);
                        cmdParam.Direction = ParameterDirection.Input;
                        cmdParam.ParameterName = paramStr + field.Key;
                        cmdParam.Value = paramValue;
                        cmdParam.DbType = field.Value.DbFieldType;

                        cmd.Parameters.Add(cmdParam);
                    }
                    if (sql.EndsWith(", ", StringComparison.OrdinalIgnoreCase))
                    {
                        sql = sql.Remove(sql.Length - 2);
                    }
                    sql += ")";

                    cmd.CommandText = sql;
                    var retVal = cmd.ExecuteNonQuery();
                    if (retVal > 0)
                    {
                        bo.ResetState();
                        if (Globals.PostInsertAction != null)
                        {
                            Globals.PostInsertAction.Invoke(cnn, bo);
                        }
                    }
                    return retVal;
                }
            }
            catch (DataException ex)
            {
                System.Diagnostics.Debug.WriteLine("Insert Exception: " + ex.Message);
                return -1;
            }
        }

        public static decimal? GetLastInsertedId(this IDbConnection cnn)
        {
            string sql = "SELECT LAST_INSERT_ID()";
            object retval;
            using (IDbCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = sql;
                retval = cmd.ExecuteScalar();
            }
            decimal newId;
            if (retval != null && decimal.TryParse(retval.ToString(), out newId))
            {
                return newId;
            }
            return null;
        }


        public static decimal? GetNextId(this IDbConnection cnn, string sequence)
        {
            string sql = "SELECT myworkshop.Nextval('" + sequence.ToLower() + "')";
            object retval;
            using (IDbCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = sql;
                retval = cmd.ExecuteScalar();
            }
            decimal newId;
            if (retval != null && decimal.TryParse(retval.ToString(), out newId))
            {
                return newId;
            }
            return null;
        }

        public static decimal?[] GetNextIdMult(this IDbConnection cnn, string sequence, int count)
        {
            decimal?[] values = new decimal?[count];
            string sql = "SELECT NETXTVAL('" + sequence + "')";
            using (IDbCommand cmd = cnn.CreateCommand())
            {
                for (int i = 0; i < count; i++)
                {
                    cmd.CommandText = sql;
                    var retval = cmd.ExecuteScalar();
                    decimal newId;
                    if (retval != null && decimal.TryParse(retval.ToString(), out newId))
                    {
                        values[i] = newId;
                    }
                }
            }
            return values;
        }

        public static int Delete<T>(this IDbConnection cnn, T bo) where T : BizObject
        {
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                string tableName = bo.Db.TableName;
                if (!string.IsNullOrEmpty(bo.Db.TableOwner))
                {
                    tableName = bo.Db.TableOwner + "." + tableName;
                }
                string sql = "DELETE FROM " + tableName;
                sql += " WHERE ";
                using (IDbCommand cmd = cnn.CreateCommand())
                {
                    foreach (var primaryKey in bo.Db.PrimaryKeys)
                    {
                        IDbDataParameter cmdParam = cmd.CreateParameter();
                        sql += primaryKey + "=" + paramStr + "PK_" + primaryKey + ", ";
                        cmdParam.ParameterName = paramStr + "PK_" + primaryKey;
                        cmdParam.Value = bo.GetFieldValue(primaryKey);
                        cmdParam.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(cmdParam);
                    }
                    if (sql.EndsWith(", ", StringComparison.OrdinalIgnoreCase))
                    {
                        sql = sql.Remove(sql.Length - 2);
                    }
                    cmd.CommandText = sql;
                    bo.ResetState();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (DataException ex)
            {
                System.Diagnostics.Debug.WriteLine("Delete Exception: " + ex.Message);
                return -1;
            }
        }
        public static int Update<T>(this IDbConnection cnn, T bo) where T : BizObject
        {
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();

                if (bo.IsDirty && bo.DirtyFields.Items.Count > 0)
                {
                    string tableName = bo.Db.TableName;
                    if (!string.IsNullOrEmpty(bo.Db.TableOwner))
                    {
                        tableName = bo.Db.TableOwner + "." + tableName;
                    }
                    string sql = "UPDATE " + tableName + " SET ";
                    int i;
                    using (IDbCommand cmd = cnn.CreateCommand())
                    {
                        foreach (var field in bo.DirtyFields.Items)
                        {
                            sql += field + "=" + paramStr + field + ", ";
                            IDbDataParameter cmdParam = cmd.CreateParameter();
                            cmdParam.ParameterName = paramStr + field;
                            cmdParam.Value = bo.GetFieldValue(field);
                            cmdParam.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(cmdParam);
                        }
                        if (sql.EndsWith(", ", StringComparison.OrdinalIgnoreCase))
                        {
                            sql = sql.Remove(sql.Length - 2);
                        }
                        sql += " WHERE ";
                        foreach (var primaryKey in bo.Db.PrimaryKeys)
                        {
                            IDbDataParameter cmdParam = cmd.CreateParameter();
                            sql += primaryKey + "=" + paramStr + "PK_" + primaryKey + ", ";
                            cmdParam.ParameterName = paramStr + "PK_" + primaryKey;
                            cmdParam.Value = bo.GetFieldValue(primaryKey);
                            cmdParam.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(cmdParam);
                        }
                        if (sql.EndsWith(", ", StringComparison.OrdinalIgnoreCase))
                        {
                            sql = sql.Remove(sql.Length - 2);
                        }
                        cmd.CommandText = sql;
                        i = cmd.ExecuteNonQuery();
                        if (i >= 0)
                        {
                            bo.EndEdit();
                        }
                    }
                    return i;
                }
                return -1;
            }
            catch (DataException ex)
            {
                System.Diagnostics.Debug.WriteLine("Update Exception: " + ex.Message);
                bo.CancelEdit();
                bo.ResetState();
                return -1;
            }
        }

        public static object GetScalarValue(this IDbConnection cnn,string query)
        {
            object result = null;
            if (cnn.State != ConnectionState.Open)
                cnn.Open();

            using (IDbCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = query;
                try
                {
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    result = null;
                    System.Diagnostics.Debug.WriteLine("GetScalar exception, " + ex.Message);
                }
            }
            return result;
        }

        public static int Save<T>(this IDbConnection cnn, T bo) where T : BizObject
        {
            if (cnn.State != ConnectionState.Open)
                cnn.Open();

            using (IDbCommand cmd = cnn.CreateCommand())
            {
                bool voidPks = false;

                cnn.FillPrimaryKeys(bo);

                switch (bo.EntityState)
                {
                    case BizObject.EntityStateType.Added:
                        if (!voidPks) return Update(cnn, bo);
                        return Insert(cnn, bo);

                    case BizObject.EntityStateType.Modified:
                        return Update(cnn, bo);

                    case BizObject.EntityStateType.Deleted:
                        return Delete(cnn, bo);
                }
            }
            return 0;
        }
        public static void FillPrimaryKeys(this IDbConnection cnn, BizObject bo)
        {
            foreach(var seq in bo.Db.Sequences)
            {
                if (bo.GetFieldValue(seq.Key) == null)
                {
                    
                    bo.SetFieldValue(seq.Key, GetSequenceValue(cnn,bo.Db.TableOwner,seq.Value));
                }
            }
        }

        public static void FillPrimaryKeys(this IDbConnection cnn, IEnumerable<BizObject> boList)
        {
            var itemsToUpdate = new List<BizObject>();
            foreach (var item in boList)
            {
                foreach (var seq in item.Db.Sequences)
                {
                    if (item.GetFieldValue(seq.Key) == null)
                    {
                        itemsToUpdate.Add(item);
                    }
                }
            }
            if (itemsToUpdate.Count > 0)
            {
                foreach (var seq in itemsToUpdate[0].Db.Sequences)
                {
                    var sequences = GetSequenceValues(cnn, itemsToUpdate[0].Db.TableOwner, seq.Value, itemsToUpdate.Count);
                    int i = 0;
                    foreach (var item in itemsToUpdate)
                    {
                        item.SetFieldValue(seq.Key, sequences[i]);
                        i++;
                    }
                }
            }
        }

        private static object GetSequenceValue(IDbConnection cnn, string owner,string sequence)
        {
            string sql = "SELECT " + owner + "." + sequence + ".NEXTVAL FROM DUAL";
            var seqVal = cnn.GetScalarValue(sql);
            return seqVal;
        }

        private async static Task<object> GetSequenceValueAsync(IDbConnection cnn, string owner, string sequence)
        {
            var items = await GetSequenceValuesAsync(cnn, owner, sequence, 1);
            if (items != null)
            {
                return items[0];
            }
            else
            {
                return null;
            }
        }

        private static IList<object> GetSequenceValues(IDbConnection cnn, string owner, string sequence,int sequenceCount)
        {
            var retVal = new List<object>();
            string sql = "SELECT " + owner + "." + sequence + ".NEXTVAL FROM DUAL CONNECT BY LEVEL <="+sequenceCount;
            using(var cmd = cnn.CreateCommand())
            {
                using(var rdr = cmd.ExecuteReader())
                {
                    retVal.Add(rdr.GetValue(0));
                }
            }
            return retVal;
        }

        private async static Task<IList<object>> GetSequenceValuesAsync(IDbConnection cnn, string owner, string sequence, int sequenceCount)
        {
            var retVal = new List<object>();
            string sql = "SELECT " + owner + "." + sequence + ".NEXTVAL FROM DUAL CONNECT BY LEVEL <=" + sequenceCount;
            using (var cmd = cnn.CreateCommand())
            {
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        retVal.Add(rdr.GetValue(0));
                    }
                }
            }
            return retVal;
        }

        public static int ExecuteNonQuery(this IDbConnection cnn, string sql)
        {
            int retVal;
            using (var cmd = cnn.CreateCommand())
            {
                cmd.CommandText = sql;

                try
                {
                    if (cnn.State != ConnectionState.Open)
                    {
                        cnn.Open();
                    }

                    retVal = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Execute non query: " + ex.Message);
                    retVal = 0;
                }
            }
            return retVal;
        }
        public static int SaveBindingList<T>(this IDbConnection cnn, BizObjectBindingList<T> datasource, bool autoCloseConnection = true) where T : BizObject
        {
            return cnn.SaveDataBinding<T>(datasource, autoCloseConnection);
        }
        public static int SaveDataBinding<T>(this IDbConnection cnn, object datasource, bool autoCloseConnection = true) where T : BizObject
        {
            var boList = datasource as BizObjectBindingList<T>;
            var retVal = 0;
            if (boList != null)
            {
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                //Pedimos las secuencias de los elementos a insertar que no tengan Pk
                if (boList.ItemsToInsert.Count> 0)
                {
                    cnn.FillPrimaryKeys(boList);
                }

                retVal += boList.ItemsToDelete.ToArray().Sum(item => Delete(cnn, item.Value));
                retVal += boList.ItemsToInsert.ToArray().Sum(item => Insert(cnn, item.Value));
                retVal += boList.ItemsToUpdate.ToArray().Sum(item => Update(cnn, item.Value));
                if (autoCloseConnection)
                    cnn.Close();
            }
            else
            {
                retVal = -1;
            }
            return retVal;
        }
    }

    

    public class QueryBindingAsyncResult<T> where T : BizObject
    {
        public readonly IDbConnection Connection;
        public BizObjectBindingList<T> Data { get; set; }

        public QueryBindingAsyncResult(IDbConnection cnn)
        {
            Connection = cnn;
        }
        public QueryBindingAsyncResult(IDbConnection cnn, BizObjectBindingList<T> data)
        {
            Connection = cnn;
            Data = data;
        }
    }

    public class QueryAsyncResult<T> where T : BizObject
    {
        public readonly IDbConnection Connection;
        public IEnumerable<T> Data { get; set; }

        public QueryAsyncResult(IDbConnection cnn)
        {
            Connection = cnn;
        }
        public QueryAsyncResult(IDbConnection cnn, IEnumerable<T> data)
        {
            Connection = cnn;
            Data = data;
        }
    }

    public class QuerySingleAsyncResult<T> where T : BizObject
    {
        public IDbConnection Connection { get; set; }
        public T Data { get; set; }

        public QuerySingleAsyncResult(IDbConnection cnn)
        {
            Connection = cnn;
        }
        public QuerySingleAsyncResult(IDbConnection cnn, T data)
        {
            Connection = cnn;
            Data = data;
        }
    }

    public class SyncToken
    {
        private int _taskCount;
        private readonly Action _action;

        public SyncToken(int taskCount, Action action)
        {
            _taskCount = taskCount;
            _action = action;
        }
        public void SetTaskDone()
        {
            _taskCount--;
            if (_taskCount <= 0)
            {
                _action.Invoke();
            }
        }
    }

    public class ReaderFieldProperties
    {
        public ReaderFieldProperties(int columnIndex, string columnName, FieldPropertyInfo propertyInfo)
        {
            ColumnIndex = columnIndex;
            ColumnName = columnName;
            PropertyInfo = propertyInfo;
        }
        public int ColumnIndex { get; internal set; }
        public string ColumnName { get; internal set; }
        public FieldPropertyInfo PropertyInfo { get; internal set; }

    }
}
