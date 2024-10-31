using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Model
{
    public class TaskComposite : ObservableCollection, ITaskComposite
    {
        private static readonly IEnumerable<string> _changedPropertyNames = [nameof(Difficult),
            nameof(Priority), nameof(Status), nameof(Deadline), nameof(Progress),
            nameof(PlannedTime), nameof(SpentTime)];

        private readonly List<ITask> _subtasks;

        private ITaskCollection? _parentTask;

        public ITask this[int index]
        {
            get => _subtasks[index];
            set => Replace(index, value);
        }

        object? IList.this[int index]
        {
            get => _subtasks[index];
            set => Replace(index, (ITask)value);
        }

        public ITaskCollection? ParentTask
        {
            get => _parentTask;
            set
            {
                if (ParentTask != value)
                {
                    _parentTask = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Difficult => CalculateDifficult();

        public int Priority => CalculatePriority();

        public DateTime? Deadline => CalculateDeadline();

        public TaskStatus Status => CalculateStatus();

        public double Progress => CalculateProgress();

        public TimeSpan PlannedTime => CalculatePlannedTime();

        public TimeSpan SpentTime => CalculateSpentTime();

        public object Metadata { get; private set; }

        public int Count => _subtasks.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        public TaskComposite(object metadata, IEnumerable<ITask>? subtasks = null)
        {
            Metadata = metadata;
            _subtasks = subtasks == null ? new() : subtasks.ToList();
            foreach (var task in _subtasks)
            {
                OnAddedItem(task, false);
            }
        }

        public bool Contains(ITask item) => _subtasks.Contains(item);

        public bool Contains(object? value) => Contains((ITask)value);

        public int IndexOf(ITask item) => _subtasks.IndexOf(item);

        public int IndexOf(object? value) => _subtasks.IndexOf((ITask)value);

        public void CopyTo(ITask[] array, int arrayIndex) => _subtasks.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index)
        {
            foreach (var item in _subtasks)
            {
                array.SetValue(item, index++);
            }
        }

        public void Add(ITask item) => AddItem(item);

        public int Add(object? value) => AddItem((ITask)value);

        public void Insert(int index, ITask item) => InsertItem(index, item);

        public void Insert(int index, object? value) => InsertItem(index, (ITask)value);

        public bool Remove(ITask item) => RemoveItem(item);

        public void Remove(object? value) => RemoveItem((ITask)value);

        public void RemoveAt(int index)
        {
            var item = _subtasks[index];
            _subtasks.RemoveAt(index);
            OnRemovedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public void Replace(int index, ITask item)
        {
            var oldItem = _subtasks[index];
            _subtasks[index] = item;
            OnRemovedItem(oldItem);
            OnAddedItem(item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        public void Move(int oldIndex, int newIndex)
        {
            var item = _subtasks[oldIndex];
            _subtasks.RemoveAt(oldIndex);
            _subtasks.Insert(newIndex, item);
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
        }

        public void Clear()
        {
            foreach (var task in _subtasks)
            {
                OnRemovedItem(task);
            }
            _subtasks.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public IEnumerator<ITask> GetEnumerator() => _subtasks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _subtasks.GetEnumerator();

        protected int AddItem(ITask item)
        {
            _subtasks.Add(item);
            var index = _subtasks.IndexOf(item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            return index;
        }

        protected void InsertItem(int index, ITask item)
        {
            _subtasks.Insert(index, item);
            OnAddedItem(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected bool RemoveItem(ITask item)
        {
            var index = _subtasks.IndexOf(item);
            var isRemoved = _subtasks.Remove(item);
            if (isRemoved)
            {
                OnRemovedItem(item);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged("Item[]");
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            }
            return isRemoved;
        }

        protected int CalculateDifficult() => _subtasks.Max(x => x.Difficult);

        protected int CalculatePriority() => _subtasks.Max(y => y.Priority);

        protected DateTime? CalculateDeadline() => _subtasks.Max(x => x.Deadline);

        protected TaskStatus CalculateStatus() => _subtasks.Min(x => x.Status);

        protected double CalculateProgress() => _subtasks.Sum(x => x.Progress) / _subtasks.Count;

        protected TimeSpan CalculatePlannedTime() =>
            _subtasks.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.PlannedTime);

        protected TimeSpan CalculateSpentTime() =>
            _subtasks.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.SpentTime);

        protected void OnAddedItem(ITask task, bool arePropertiesUpdate = true)
        {
            task.ParentTask = this;
            if (task is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }
            if(arePropertiesUpdate)
            {
                foreach (var propertyName in _changedPropertyNames)
                {
                    OnPropertyChanged(propertyName);
                }
            }
        }

        protected void OnRemovedItem(ITask task)
        {
            task.ParentTask = null;
            if (task is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged -= Notify_PropertyChanged;
            }
            foreach (var propertyName in _changedPropertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        private void Notify_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_changedPropertyNames.Contains(e.PropertyName))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
