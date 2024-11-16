using System.ComponentModel;

namespace Model
{
    public class TaskComposite : TrackableCollection<ITask>, ITaskComposite, ICloneable
    {
        private static readonly IEnumerable<string> _changedPropertyNames = [nameof(Difficult),
            nameof(Priority), nameof(Status), nameof(Deadline), nameof(Progress),
            nameof(PlannedTime), nameof(SpentTime)];

        private IFullCollection<ITask>? _parentTask;

        private object _metadata;

        public IFullCollection<ITask>? ParentTask
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

        public int Difficult => _items.Max(x => x.Difficult);

        public int Priority => _items.Max(y => y.Priority);

        public DateTime? Deadline => _items.Max(x => x.Deadline);

        public TaskStatus Status => _items.Min(x => x.Status);

        public double Progress => _items.Sum(x => x.Progress) / _items.Count;

        public TimeSpan PlannedTime =>
            _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.PlannedTime);

        public TimeSpan SpentTime =>
            _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.SpentTime);

        public object Metadata
        {
            get => _metadata;
            set => UpdateProperty(ref _metadata, value);
        }

        public TaskComposite(IEnumerable<ITask>? subtasks = null) : base(subtasks) { }

        protected override void OnAddedItem(ITask task, bool arePropertiesUpdate = true)
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

        protected override void OnRemovedItem(ITask task)
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

        public object Clone()
        {
            var copyList = new List<ITask>();
            foreach (var task in this)
            {
                if (task is ICloneable taskCloneable)
                {
                    copyList.Add((ITask)taskCloneable.Clone());
                }
            }
            var result = new TaskComposite(copyList);
            if (Metadata is ICloneable metadataCloneable)
            {
                result.Metadata = metadataCloneable.Clone();
            }
            return result;
        }
    }
}
