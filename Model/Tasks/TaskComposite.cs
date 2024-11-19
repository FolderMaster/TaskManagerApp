using System.ComponentModel;

using Model.Interfaces;
using Model.Technicals;

namespace Model.Tasks
{
    public class TaskComposite : TrackableCollection<ITask>, ITaskComposite, ICloneable
    {
        private static readonly IEnumerable<string> _changedPropertyNames = [ nameof(Difficult),
            nameof(Priority), nameof(Status), nameof(Deadline), nameof(Progress),
            nameof(PlannedTime), nameof(SpentTime) ];

        private IList<ITask>? _parentTask;

        private object? _metadata;

        public IList<ITask>? ParentTask
        {
            get => _parentTask;
            set => UpdateProperty(ref _parentTask, value);
        }

        public object? Metadata
        {
            get => _metadata;
            set => UpdateProperty(ref _metadata, value);
        }

        public int Difficult => _items.Count > 0 ? _items.Max(x => x.Difficult) : 0;

        public int Priority => _items.Count > 0 ? _items.Max(y => y.Priority) : 0;

        public TaskStatus Status => _items.Count > 0 ? _items.Min(x => x.Status) : TaskStatus.Planned;

        public DateTime? Deadline => _items.Count > 0 ? _items.Max(x => x.Deadline) : null;

        public double Progress => _items.Count > 0 ? _items.Sum(i => i.Progress) / _items.Count : 0;

        public TimeSpan PlannedTime => _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.PlannedTime);

        public TimeSpan SpentTime => _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.SpentTime);

        public TaskComposite(IEnumerable<ITask>? subtasks = null) : base(subtasks) { }

        public TaskComposite() : this(null) { }

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

        protected override void OnAddedItem(ITask task, bool arePropertiesUpdate = true)
        {
            task.ParentTask = this;
            if (task is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Notify_PropertyChanged;
            }
            if (arePropertiesUpdate)
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
    }
}
