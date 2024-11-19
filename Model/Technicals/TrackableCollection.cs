using System.Collections;
using System.Collections.Specialized;

namespace Model.Technicals
{
    public class TrackableCollection<T> : TrackableObject,
        IList<T>, IList, INotifyCollectionChanged
    {
        protected readonly List<T> _items = new();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public T this[int index]
        {
            get => _items[index];
            set => Replace(index, value);
        }

        object? IList.this[int index]
        {
            get => _items[index];
            set => Replace(index, (T)value);
        }

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        public TrackableCollection(IEnumerable<T>? items = null)
        {
            if (items?.Any() == true)
            {
                _items = items.ToList();
                foreach (var task in _items)
                {
                    OnAddedItem(task, false);
                }
            }
        }

        public bool Contains(T item) => _items.Contains(item);

        public bool Contains(object? value) => Contains((T)value);

        public int IndexOf(T item) => _items.IndexOf(item);

        public int IndexOf(object? value) => _items.IndexOf((T)value);

        public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index)
        {
            foreach (var item in _items)
            {
                array.SetValue(item, index++);
            }
        }

        public void Add(T item) => AddItem(item);

        public int Add(object? value) => AddItem((T)value);

        public void Insert(int index, T item) => InsertItem(index, item);

        public void Insert(int index, object? value) => InsertItem(index, (T)value);

        public bool Remove(T item) => RemoveItem(item);

        public void Remove(object? value) => RemoveItem((T)value);

        public void RemoveAt(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);
            OnRemovedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public void Replace(int index, T item)
        {
            var oldItem = _items[index];
            _items[index] = item;
            OnRemovedItem(oldItem);
            OnAddedItem(item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        public void Move(int oldIndex, int newIndex)
        {
            var item = _items[oldIndex];
            _items.RemoveAt(oldIndex);
            _items.Insert(newIndex, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
        }

        public void Clear()
        {
            foreach (var task in _items)
            {
                OnRemovedItem(task);
            }
            _items.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        protected int AddItem(T item)
        {
            _items.Add(item);
            var index = _items.IndexOf(item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            return index;
        }

        protected void InsertItem(int index, T item)
        {
            _items.Insert(index, item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected bool RemoveItem(T item)
        {
            var index = _items.IndexOf(item);
            var isRemoved = _items.Remove(item);
            if (isRemoved)
            {
                OnRemovedItem(item);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            return isRemoved;
        }

        protected virtual void OnAddedItem(T item, bool arePropertiesUpdate = true) { }

        protected virtual void OnRemovedItem(T item) { }

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? oldItem, object? newItem, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, newItem, oldItem, index));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action,
            object? item, int index, int oldIndex) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs
                (action, item, index, oldIndex));

        protected void OnCollectionChanged(NotifyCollectionChangedAction action) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs args) =>
            CollectionChanged?.Invoke(this, args);
    }
}
