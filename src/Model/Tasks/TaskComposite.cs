using System.ComponentModel;

using TrackableFeatures;

using Model.Interfaces;

namespace Model.Tasks
{
    /// <summary>
    /// Класс составной задачи. Наследует <see cref="TrackableCollection{ITask}"/>.
    /// Реализует <see cref="ITaskComposite"/> и <see cref="ICloneable"/>.
    /// </summary>
    public class TaskComposite : TrackableCollection<ITask>, ITaskComposite, ICloneable
    {
        /// <summary>
        /// Названия изменящихся свойств.
        /// </summary>
        private static readonly IEnumerable<string> _changedPropertyNames = [ nameof(Difficult),
            nameof(Priority), nameof(Status), nameof(Deadline), nameof(Progress),
            nameof(PlannedTime), nameof(SpentTime) ];

        /// <summary>
        /// Родительская задача.
        /// </summary>
        private IList<ITask>? _parentTask;

        /// <summary>
        /// Метаданные.
        /// </summary>
        private object? _metadata;

        /// <inheritdoc/>
        public IList<ITask>? ParentTask
        {
            get => _parentTask;
            set => UpdateProperty(ref _parentTask, value);
        }

        /// <inheritdoc/>
        public object? Metadata
        {
            get => _metadata;
            set => UpdateProperty(ref _metadata, value);
        }

        /// <inheritdoc/>
        public int Difficult => _items.Count > 0 ? _items.Max(x => x.Difficult) : 0;

        /// <inheritdoc/>
        public int Priority => _items.Count > 0 ? _items.Max(y => y.Priority) : 0;

        /// <inheritdoc/>
        public TaskStatus Status => _items.Count > 0 ? _items.Min(x => x.Status) : TaskStatus.Planned;

        /// <inheritdoc/>
        public DateTime? Deadline => _items.Count > 0 ? _items.Max(x => x.Deadline) : null;

        /// <inheritdoc/>
        public double Progress => _items.Count > 0 ? _items.Sum(i => i.Progress) / _items.Count : 0;

        /// <inheritdoc/>
        public TimeSpan PlannedTime => _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.PlannedTime);

        /// <inheritdoc/>
        public TimeSpan SpentTime => _items.Aggregate(TimeSpan.Zero, (sum, interval) => sum + interval.SpentTime);

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskComposite"/>.
        /// </summary>
        /// <param name="subtasks">Подзадачи.</param>
        public TaskComposite(IEnumerable<ITask>? subtasks = null) : base(subtasks) { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskComposite"/> по умолчанию.
        /// </summary>
        public TaskComposite() : this(null) { }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
