using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Model
{
    public class TimeIntervalCollection : ObservableCollection,
        IFullCollection<ITimeInterval>, ITimeInterval
    {
        private readonly List<ITimeInterval> _timeIntervals = new();

        public ITimeInterval this[int index]
        {
            get => _timeIntervals[index];
            set => Replace(index, value);
        }

        object? IList.this[int index]
        {
            get => _timeIntervals[index];
            set => Replace(index, (TimeInterval)value);
        }

        public TimeSpan Duration =>
            _timeIntervals.Aggregate(TimeSpan.Zero, (s, i) => s + i.Duration);

        public int Count => _timeIntervals.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        public TimeIntervalCollection(IEnumerable<ITimeInterval>? timeIntervals = null)
        {
            _timeIntervals = timeIntervals == null ? new() : timeIntervals.ToList();
            foreach (var task in _timeIntervals)
            {
                OnAddedItem(task, false);
            }
        }

        public bool Contains(ITimeInterval item) => _timeIntervals.Contains(item);

        public bool Contains(object? value) => Contains((ITimeInterval)value);

        public int IndexOf(ITimeInterval item) => _timeIntervals.IndexOf(item);

        public int IndexOf(object? value) => _timeIntervals.IndexOf((ITimeInterval)value);

        public void CopyTo(ITimeInterval[] array, int arrayIndex) =>
            _timeIntervals.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index)
        {
            foreach (var item in _timeIntervals)
            {
                array.SetValue(item, index++);
            }
        }

        public void Add(ITimeInterval item) => AddItem(item);

        public int Add(object? value) => AddItem((ITimeInterval)value);

        public void Insert(int index, ITimeInterval item) => InsertItem(index, item);

        public void Insert(int index, object? value) => InsertItem(index, (ITimeInterval)value);

        public bool Remove(ITimeInterval item) => RemoveItem(item);

        public void Remove(object? value) => RemoveItem((ITimeInterval)value);

        public void RemoveAt(int index)
        {
            var item = _timeIntervals[index];
            _timeIntervals.RemoveAt(index);
            OnRemovedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public void Replace(int index, ITimeInterval item)
        {
            var oldItem = _timeIntervals[index];
            _timeIntervals[index] = item;
            OnRemovedItem(oldItem);
            OnAddedItem(item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        public void Move(int oldIndex, int newIndex)
        {
            var item = _timeIntervals[oldIndex];
            _timeIntervals.RemoveAt(oldIndex);
            _timeIntervals.Insert(newIndex, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
        }

        public void Clear()
        {
            foreach (var task in _timeIntervals)
            {
                OnRemovedItem(task);
            }
            _timeIntervals.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public IEnumerator<ITimeInterval> GetEnumerator() => _timeIntervals.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _timeIntervals.GetEnumerator();

        protected int AddItem(ITimeInterval item)
        {
            _timeIntervals.Add(item);
            var index = _timeIntervals.IndexOf(item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            return index;
        }

        protected void InsertItem(int index, ITimeInterval item)
        {
            _timeIntervals.Insert(index, item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected bool RemoveItem(ITimeInterval item)
        {
            var index = _timeIntervals.IndexOf(item);
            var isRemoved = _timeIntervals.Remove(item);
            if (isRemoved)
            {
                OnRemovedItem(item);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            return isRemoved;
        }

        protected void OnAddedItem(ITimeInterval timeInterval, bool arePropertiesUpdate = true)
        {
            if (timeInterval is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }
            if (arePropertiesUpdate)
            {
                OnPropertyChanged(nameof(Duration));
            }
        }

        protected void OnRemovedItem(ITimeInterval timeInterval)
        {
            if (timeInterval is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged -= Notify_PropertyChanged;
            }
            OnPropertyChanged(nameof(Duration));
        }

        private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Duration))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
