using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using Common.ExpressionEngine;

namespace Data
{
    public class BizObjectBindingList<T> : BindingList<T>, IBindingListView, INotifyPropertyChanged, IDisposable where T : BizObject
    {
        private bool _filterSuspended;
        private bool _doNotResetDeltas;
        private bool _loadingData;
        private readonly List<T> _innerList = new List<T>();
        private bool _disposing;
        private bool _transactionStarted;
        private bool _transactionCommited;
        private bool _transactionRolledBack;
        private Predicate<T> _filterPredicate;

        public const string ExpressionFilteringHeader = "()=>";

        public List<T> OriginalList
        {
            get
            { return _innerList; }
        }

        public IList<T> GetItems()
        {
            return Items;
        }

        private Dictionary<string, T> _itemsUpdated;
        private Dictionary<string, T> _itemsDeleted;
        private Dictionary<string, T> _itemsAdded;

        private Dictionary<T, BizObjectState> _bckitemsUpdated;
        private Dictionary<T, BizObjectState> _bckitemsDeleted;
        private Dictionary<T, BizObjectState> _bckitemsAdded;

        private bool _addingNew;
        private T _newObject;
        private bool _listChangedEventsBuff;

        //private Dictionary<string, BizObjectBindingList<BizObject>> _childrens;

        public BizObjectBindingList()
        {
            _transactionStarted = false;
            _transactionCommited = false;
            _transactionRolledBack = false;
            _loadingData = true;
            _itemsUpdated = new Dictionary<string, T>();
            _itemsAdded = new Dictionary<string, T>();
            _itemsDeleted = new Dictionary<string, T>();
            ListChanged += BizObjectListChanged;
            _loadingData = false;
            _listChangedEventsBuff = RaiseListChangedEvents;


        }

        public void SuspendFilter()
        {
            _filterSuspended = false;
        }

        public void ResumeFilter()
        {
            _filterSuspended = false;
            RefreshFilter();
        }

        public void RefreshFilter()
        {
            var filter = Filter;
            RemoveFilter();
            Filter = filter;
        }

        public void SuspendRaiseEvents()
        {
            _listChangedEventsBuff = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
        }

        public void ResumeRaiseEvents()
        {
            RaiseListChangedEvents = _listChangedEventsBuff;
        }

        public BizObjectBindingList(IEnumerable<T> enumerable)
        {
            _loadingData = true;
            _transactionStarted = false;
            _transactionCommited = false;
            _transactionRolledBack = false;

            SuspendRaiseEvents();

            foreach (var item in enumerable)
            {
                Add(item);
                _innerList.Add(item);
            }

            ResumeRaiseEvents();

            _itemsUpdated = new Dictionary<string, T>();
            _itemsAdded = new Dictionary<string, T>();
            _itemsDeleted = new Dictionary<string, T>();

            ListChanged += BizObjectListChanged;
            _loadingData = false;
        }

        public BizObjectBindingList(IList<T> list)
        { 
            _loadingData = true;
            _transactionStarted = false;
            _transactionCommited = false;
            _transactionRolledBack = false;

            SuspendRaiseEvents();

            foreach (var item in list)
            {
                Add(item);
                _innerList.Add(item);
            }

            ResumeRaiseEvents();

            _itemsUpdated = new Dictionary<string, T>();
            _itemsAdded = new Dictionary<string, T>();
            _itemsDeleted = new Dictionary<string, T>();

            ListChanged += BizObjectListChanged;
            _loadingData = false;
        }

        public Dictionary<string, T> ItemsToUpdate
        {
            get { return _itemsUpdated; }
        }

        public Dictionary<string, T> ItemsToDelete
        {
            get { return _itemsDeleted; }
        }

        public Dictionary<string, T> ItemsToInsert
        {
            get { return _itemsAdded; }
        }

        protected override void RemoveItem(int index)
        {
            if (_addingNew)
            {
                CancelNew(index);
            }
            else
            {
                base[index].EntityState=BizObject.EntityStateType.Deleted;

                _itemsAdded.Remove(base[index].UniqueId);
                _itemsUpdated.Remove(base[index].UniqueId);
                if (!_itemsDeleted.ContainsKey(base[index].UniqueId))
                {
                    System.Diagnostics.Debug.WriteLine("Item of Type[" + base[index].GetType().Name + "] Deleted");
                    _itemsDeleted.Add(base[index].UniqueId, base[index]);
                }
                _innerList.Remove(base[index]);
                base.RemoveItem(index);
            }
        }


        protected override object AddNewCore()
        {
            _addingNew = true;
            var newObject = base.AddNewCore() as T;
            if (!_loadingData && newObject != null)
            {
                newObject.EntityState=BizObject.EntityStateType.Added;
                _itemsAdded[newObject.UniqueId] = newObject;
                _newObject = newObject;
            }
            return newObject;
        }


        public override void CancelNew(int itemIndex)
        {
            if (_itemsAdded.ContainsKey(base[itemIndex].UniqueId))
            {
                System.Diagnostics.Debug.WriteLine("Item of Type[" + _newObject.GetType().Name + "] removed from AddNew");
                _itemsAdded.Remove(base[itemIndex].UniqueId);
            }
            _innerList.Remove(base[itemIndex]);
            _addingNew = false;
            base.CancelNew(itemIndex);
        }

        private void BizObjectListChanged(object sender, ListChangedEventArgs e)
        {
            if (!RaiseListChangedEvents) return;

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    if (e.NewIndex >= 0)
                    {
                        if (!_itemsAdded.ContainsKey(base[e.NewIndex].UniqueId))
                        {
                            _itemsUpdated[base[e.NewIndex].UniqueId] = base[e.NewIndex];
                        }
                    }
                    if (e.PropertyDescriptor != null && !_loadingData)
                    {
                        if (e.PropertyDescriptor.Name == "EntityState")
                        {
                            if (_itemsAdded.ContainsKey(base[e.NewIndex].UniqueId) && base[e.NewIndex].EntityState != BizObject.EntityStateType.Added)
                            {
                                _itemsAdded.Remove(base[e.NewIndex].UniqueId);
                            }
                            else if (_itemsDeleted.ContainsKey(base[e.NewIndex].UniqueId) && base[e.NewIndex].EntityState != BizObject.EntityStateType.Deleted)
                            {
                                _itemsDeleted.Remove(base[e.NewIndex].UniqueId);
                            }
                            else if (_itemsUpdated.ContainsKey(base[e.NewIndex].UniqueId) && base[e.NewIndex].EntityState != BizObject.EntityStateType.Modified)
                            {
                                _itemsUpdated.Remove(base[e.NewIndex].UniqueId);
                            }
                        }
                        RaisePropertyChanged(e.PropertyDescriptor.Name);
                    }
                    break;
                case ListChangedType.Reset:
                    if (_doNotResetDeltas) return;
                    System.Diagnostics.Debug.WriteLine("Resetting List");
                    _itemsAdded.Clear();
                    _itemsDeleted.Clear();
                    _itemsUpdated.Clear();
                    break;
            }
        }


        public void ClearChanges()
        {
            System.Diagnostics.Debug.WriteLine("Clearing Changes");
            _itemsAdded.Clear();
            _itemsDeleted.Clear();
            _itemsUpdated.Clear();
        }

        public void ClearInserts()
        {
            System.Diagnostics.Debug.WriteLine("Clearing Inserts");
            _itemsAdded.Clear();
        }

        public void ClearUpdates()
        {
            System.Diagnostics.Debug.WriteLine("Clearing Updates");
            _itemsUpdated.Clear();
        }

        public void ClearDeletes()
        {
            System.Diagnostics.Debug.WriteLine("Clearing Deletes");
            _itemsDeleted.Clear();
        }


        #region Searching

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);

            if (key != null)
            {
                // Loop through the items to see if the key
                // value matches the property value.
                for (int i = 0; i < Count; ++i)
                {
                    T item = Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }

        public int Find(string property, object key)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            return FindCore(prop, key);
        }

        #endregion Searching

        #region Sorting
        ArrayList _sortedList;
        BizObjectBindingList<T> _unsortedItems;
        bool _isSortedValue;
        ListSortDirection _sortDirectionValue;
        PropertyDescriptor _sortPropertyValue;

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return _isSortedValue; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirectionValue; }
        }


        public void ApplySort(string propertyName, ListSortDirection direction)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T))[propertyName];

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                throw new ArgumentException(propertyName +
                                            " is not a valid property for type:" + typeof(T).Name);
            ApplySortCore(prop, direction);
        }

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction)
        {
            _doNotResetDeltas = true;
            _sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                _sortPropertyValue = prop;
                _sortDirectionValue = direction;

                _unsortedItems = new BizObjectBindingList<T>();

                if (_sortPropertyValue != null)
                {
                    // Loop through each item, adding it the the sortedItems ArrayList.
                    foreach (T item in Items)
                    {
                        _unsortedItems.Add(item);
                        var val = prop.GetValue(item);
                        if (val!=null)
                            _sortedList.Add(val);
                    }
                }
                // Call Sort on the ArrayList.
                _sortedList.Sort();

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    _sortedList.Reverse();

                for (int i = 0; i < Count; i++)
                {
                    int position = Find(prop.Name, _sortedList[i]);
                    if (position != i && position > 0)
                    {
                        T temp = this[i];
                        this[i] = this[position];
                        this[position] = temp;
                    }
                }

                _isSortedValue = true;

                // If the list does not have a filter applied, 
                // raise the ListChanged event so bound controls refresh their
                // values. Pass -1 for the index since this is a Reset.
                if (String.IsNullOrEmpty(Filter))
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

            }
            else
                // If the property type does not implement IComparable, let the user
                // know.
                throw new InvalidOperationException("Cannot sort by "
                    + prop.Name + ". This" + prop.PropertyType +
                    " does not implement IComparable");
            _doNotResetDeltas = false;
        }

        protected override void RemoveSortCore()
        {
            _doNotResetDeltas = true;
            // Ensure the list has been sorted.
            if (_unsortedItems != null && _innerList.Count > 0)
            {
                Clear();
                if (Filter != null)
                {
                    _unsortedItems.Filter = Filter;
                    foreach (T item in _unsortedItems)
                        Add(item);
                }
                else
                {
                    foreach (T item in _innerList)
                        Add(item);
                }
                _isSortedValue = false;
                // Raise the list changed event, indicating a reset, and index
                // of -1.

                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset,
                    -1));

            }
            _doNotResetDeltas = false;
        }

        public void RemoveSort()
        {
            RemoveSortCore();
        }


        public override void EndNew(int itemIndex)
        {
            // Check to see if the item is added to the end of the list,
            // and if so, re-sort the list.
            if (IsSortedCore && itemIndex > 0
                && itemIndex == Count - 1)
            {
                ApplySortCore(_sortPropertyValue,
                    _sortDirectionValue);
                base.EndNew(itemIndex);
            }

            _addingNew = false;
        }

        #endregion Sorting

        #region AdvancedSorting
        public bool SupportsAdvancedSorting
        {
            get { return false; }
        }
        public ListSortDescriptionCollection SortDescriptions
        {
            get { return null; }
        }

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new NotSupportedException();
        }

        #endregion AdvancedSorting

        #region Filtering

        public bool SupportsFiltering
        {
            get { return true; }
        }

        public void RemoveFilter()
        {
// ReSharper disable RedundantCheckBeforeAssignment
            if (Filter != null) Filter = null;
// ReSharper restore RedundantCheckBeforeAssignment
        }

        private string _filterValue;

        public string Filter
        {
            get
            {
                return _filterValue;
            }
            set
            {
                System.Diagnostics.Debug.WriteLine("Start Filtering Data [" + DateTime.Now.ToLongTimeString() + "]");
                if (_filterSuspended) return;

                if (_filterValue == value) return;

                _doNotResetDeltas = true;
                _filterValue = value;

                //if (this.Items.Count == _originalListValue.Count && String.IsNullOrEmpty(value)) return;

                //Turn off list-changed events.

                if (String.IsNullOrEmpty(value))
                {
                    ResetList();
                    return;
                }

                ClearItems();

                if (!String.IsNullOrEmpty(value) && value.StartsWith(ExpressionFilteringHeader))
                {
                    //**************************
                    //Expression Filtering...
                    //**************************
                    try
                    {
                        SuspendRaiseEvents();

                        string filter = _filterValue.Substring(ExpressionFilteringHeader.Length);
                        System.Diagnostics.Debug.WriteLine("Expression Filter " + filter);
                        var exprFilter = ExpressionBuilder.BuildFunctor<T, bool>(filter);
                        _filterPredicate = new Predicate<T>(exprFilter);
                        var items = _innerList.FindAll(_filterPredicate);
                        foreach (var item in items)
                        {
                            Add(item);
                        }

                        ResumeRaiseEvents();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Parse Filter Exception: " + ex.Message);
                        ClearItems();
                    }
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
                else
                {

                    //**************************
                    //Traditional Filtering...
                    //**************************
                    // If the value is not null or empty, but doesn't
                    // match expected format, throw an exception.
                    if (!string.IsNullOrEmpty(value) &&
                        !Regex.IsMatch(value,
                                       BuildRegExForFilterFormat(), RegexOptions.Singleline))
                    {
                        _loadingData = false;
                        throw new ArgumentException("Filter is not in " +
                                                    "the format: propName[<>=]'value'.");
                    }

                    SuspendRaiseEvents();

                    foreach (var item in _innerList)
                    {
                        Add(item);
                    }

                    value = value.ToUpper();
                    int count = 0;
                    string[] matches = value.ToUpper().Split(new[] { " AND " },
                                                                StringSplitOptions.RemoveEmptyEntries);
                    System.Diagnostics.Debug.WriteLine("Start Applying Filter [" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");
                    while (count < matches.Length)
                    {
                        string filterPart = matches[count];

                        System.Diagnostics.Debug.WriteLine("Start Parsing Filter [" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");
                        SingleFilterInfo filterInfo = ParseFilter(filterPart);
                        System.Diagnostics.Debug.WriteLine("End Parsing Filter [" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");
                        _doNotResetDeltas = true;
                        System.Diagnostics.Debug.WriteLine("Start Applying Filter " + filterInfo.PropName + " " + filterInfo.OperatorValue + " " + filterInfo.CompareValue + "[" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");

                        ApplyFilter(filterInfo);

                        System.Diagnostics.Debug.WriteLine("End Applying Filter [" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");
                        count++;
                    }
                    System.Diagnostics.Debug.WriteLine("Start Applying Filter [" + DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond + "]");


                    _doNotResetDeltas = true;

                    ResumeRaiseEvents();
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
                System.Diagnostics.Debug.WriteLine("End Filtering Data [" + DateTime.Now.ToLongTimeString() + "]");
                _doNotResetDeltas = false;
                _loadingData = false;
            }
        }

        // Build a regular expression to determine if 
        // filter is in correct format.
        public static string BuildRegExForFilterFormat()
        {
            var regex = new StringBuilder();

            // Look for optional literal brackets, 
            // followed by word characters or space.
            regex.Append(@"\[?[\w\s]+\]?\s?");

            // Add the operators: > < ! or =.
            regex.Append(@"[><=!]");

            //Add optional space followed by optional quote and
            // any character followed by the optional quote.
            regex.Append(@"\s?'?.+'?");

            return regex.ToString();
        }



        private void ResetList()
        {
            SuspendRaiseEvents();

            Items.Clear();

            foreach (T t in _innerList)
            {
                Add(t);
            }

            if (IsSortedCore && SortPropertyCore!=null)
                ApplySortCore(SortPropertyCore, SortDirectionCore);

            ResumeRaiseEvents();
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (!RaiseListChangedEvents) return;
            if (_loadingData) return;

            // If the list is reset, check for a filter. If a filter 
            // is applied don't allow items to be added to the list.
            if (e.ListChangedType == ListChangedType.Reset)
            {
                AllowNew = string.IsNullOrEmpty(Filter);
            }
            // Add the new item to the original list.
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                lock (_innerList)
                {
                    SuspendFilter();
                    if (!_innerList.Contains(this[e.NewIndex]))
                    {
                        _innerList.Add(this[e.NewIndex]);
                        if (!String.IsNullOrEmpty(Filter))
                        {
                            string cachedFilter = Filter;
                            Filter = cachedFilter;
                        }
                    }
                }
            }
            // Remove the new item from the original list.
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (_innerList.Count < e.NewIndex)
                    _innerList.RemoveAt(e.NewIndex);
            }

            base.OnListChanged(e);
        }


        internal void ApplyFilter(SingleFilterInfo filterParts)
        {
            _doNotResetDeltas = true;


            IList<T> results = new List<T>();
            // Check each value and add to the results list.

            foreach (T item in this)
            {
                var compareValue = filterParts.GetValueOf(item) as IComparable;
                //if (filterParts.PropDesc.GetValue(item) != null)
                if (compareValue != null)
                {
                    // IComparable compareValue =
                    //    filterParts.GetValueOf(item) as IComparable;
                    int result =
                        compareValue.CompareTo(filterParts.CompareValue);
                    if (filterParts.OperatorValue ==
                        FilterOperator.EqualTo && result == 0)
                        results.Add(item);
                    else if (filterParts.OperatorValue ==
                        FilterOperator.GreaterThan && result > 0)
                        results.Add(item);
                    else if (filterParts.OperatorValue ==
                        FilterOperator.LessThan && result < 0)
                        results.Add(item);
                    else if (filterParts.OperatorValue ==
                        FilterOperator.NotEqualTo && result != 0)
                        results.Add(item);
                }
            }

            ClearItems();

            foreach (var itemFound in results)
            {
                if (itemFound != null)
                {
                    Add(itemFound);
                }
            }

            _doNotResetDeltas = false;
        }

        internal SingleFilterInfo ParseFilter(string filterPart)
        {

            var filterInfo = new SingleFilterInfo {OperatorValue = DetermineFilterOperator(filterPart)};

            string[] filterStringParts =
                filterPart.Split(new[] { (char)filterInfo.OperatorValue });

            filterInfo.PropName =
                filterStringParts[0].Replace("[", "").
                Replace("]", "").Replace(" AND ", "").Trim();

            // Get the property descriptor for the filter property name.
            PropertyDescriptor filterPropDesc =
                TypeDescriptor.GetProperties(typeof(T))[filterInfo.PropName];

            PropertyInfo pi = typeof(T).GetProperty(filterInfo.PropName);

            filterInfo.GetValueOf = Reflection.CreateGetMethod(pi);

            // Convert the filter compare value to the property type.
            if (filterPropDesc == null)
                throw new InvalidOperationException("Specified property to " +
                    "filter " + filterInfo.PropName +
                    " on does not exist on type: " + typeof(T).Name);

            //filterInfo.PropDesc = filterPropDesc;

            string comparePartNoQuotes = StripOffQuotes(filterStringParts[1]);
            try
            {
                TypeConverter converter =
                    TypeDescriptor.GetConverter(filterPropDesc.PropertyType);
                filterInfo.CompareValue =
                    converter.ConvertFromString(comparePartNoQuotes);
            }
            catch (NotSupportedException)
            {
                throw new InvalidOperationException("Specified filter" +
                    "value " + comparePartNoQuotes + " can not be converted" +
                    "from string. Implement a type converter for " +
                    filterPropDesc.PropertyType);
            }
            return filterInfo;
        }

        internal FilterOperator DetermineFilterOperator(string filterPart)
        {
            // Determine the filter's operator.
            if (Regex.IsMatch(filterPart, "[^>^<]="))
                return FilterOperator.EqualTo;
            if (Regex.IsMatch(filterPart, "<[^>^=]"))
                return FilterOperator.LessThan;
            if (Regex.IsMatch(filterPart, "[^<]>[^=]"))
                return FilterOperator.GreaterThan;
            if (Regex.IsMatch(filterPart, "[^<]![^=]"))
                return FilterOperator.NotEqualTo;
            return FilterOperator.None;
        }

        internal static string StripOffQuotes(string filterPart)
        {
            // Strip off quotes in compare value if they are present.
            if (Regex.IsMatch(filterPart, "'.+'"))
            {
                int quote = filterPart.IndexOf('\'');
                filterPart = filterPart.Remove(quote, 1);
                quote = filterPart.LastIndexOf('\'');
                filterPart = filterPart.Remove(quote, 1);
                filterPart = filterPart.Trim();
            }
            return filterPart;
        }

        #endregion Filtering

        public void BeginTransaction()
        {
            if (_transactionStarted) return;

            _transactionStarted = true;
            _transactionRolledBack = false;
            _transactionCommited = false;

            System.Diagnostics.Debug.WriteLine("Begining Transaction");
            _bckitemsAdded = new Dictionary<T, BizObjectState>();
            foreach (var item in _itemsAdded)
            {
                var state = new BizObjectState(item.Value);
                _bckitemsAdded.Add(item.Value, state);
            }
            _bckitemsDeleted = new Dictionary<T, BizObjectState>();
            foreach (var item in _itemsDeleted)
            {
                var state = new BizObjectState(item.Value);
                _bckitemsDeleted.Add(item.Value, state);
            }
            _bckitemsUpdated = new Dictionary<T, BizObjectState>();
            foreach (var item in _itemsUpdated)
            {
                var state = new BizObjectState(item.Value);
                _bckitemsUpdated.Add(item.Value, state);
            }
        }

        public void CommitTransaction()
        {
            if (_transactionCommited) return;

            _transactionStarted = false;
            _transactionCommited = true;
            System.Diagnostics.Debug.WriteLine("Committing Transaction");
            _bckitemsAdded.Clear();
            _bckitemsDeleted.Clear();
            _bckitemsUpdated.Clear();
        }

        public void RollbackTransaction()
        {
            if (_transactionRolledBack) return;

            _transactionStarted = false;
            _transactionRolledBack = true;
            System.Diagnostics.Debug.WriteLine("Rolling Back Transaction");
            _itemsAdded = new Dictionary<string, T>();
            foreach (var item in _bckitemsAdded)
            {
                item.Key.DirtyFields = item.Value.DirtyFields;
                item.Key.EntityState = item.Value.State;
                _itemsAdded.Add(item.Key.UniqueId, item.Key);
            }
            _itemsDeleted = new Dictionary<string, T>();
            foreach (var item in _bckitemsDeleted)
            {
                item.Key.DirtyFields = item.Value.DirtyFields;
                item.Key.EntityState = item.Value.State;
                _itemsDeleted.Add(item.Key.UniqueId, item.Key);
            }
            _itemsUpdated = new Dictionary<string, T>();
            foreach (var item in _bckitemsUpdated)
            {
                item.Key.DirtyFields = item.Value.DirtyFields;
                item.Key.EntityState = item.Value.State;
                _itemsUpdated.Add(item.Key.UniqueId, item.Key);
            }
            _bckitemsAdded.Clear();
            _bckitemsDeleted.Clear();
            _bckitemsUpdated.Clear();
        }

        public void Dispose()
        {
            if (!_disposing)
            {
                _disposing = true;
                if (_innerList != null)
                    _innerList.Clear();
                if (_innerList != null)
                    _innerList.Clear();
                if (_itemsUpdated != null)
                    _itemsUpdated.Clear();
                if (_itemsDeleted != null)
                    _itemsDeleted.Clear();
                if (_itemsAdded != null)
                    _itemsAdded.Clear();
                if (_bckitemsUpdated != null)
                    _bckitemsUpdated.Clear();
                if (_bckitemsDeleted != null)
                    _bckitemsDeleted.Clear();
                if (_bckitemsAdded != null)
                    _bckitemsAdded.Clear();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (!RaiseListChangedEvents) return;
            System.Diagnostics.Debug.WriteLine("Raising Property Changed");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }
    }

    public struct SingleFilterInfo
    {
        internal string PropName;
        //internal PropertyDescriptor PropDesc;
        internal Reflection.GenericGetter GetValueOf;
        internal Object CompareValue;
        internal FilterOperator OperatorValue;
    }

    // Enum to hold filter operators. The chars 
    // are converted to their integer values.
    public enum FilterOperator
    {
        EqualTo = '=',
        LessThan = '<',
        GreaterThan = '>',
        NotEqualTo = '!',
        None = ' '
    }

    internal class BizObjectState
    {
        public BizObject.EntityStateType State { get; set; }
        public BizObjectDirtyFields DirtyFields { get; set; }

        public BizObjectState(IEnumerable<string> dirtyFields, BizObject.EntityStateType state)
        {
            State = state;
            DirtyFields = new BizObjectDirtyFields(dirtyFields);
        }

        public BizObjectState(BizObject obj)
        {
            State = obj.EntityState;
            DirtyFields = obj.DirtyFields;
        }
    }
}
