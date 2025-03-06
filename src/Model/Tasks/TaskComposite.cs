using System.ComponentModel;

using TrackableFeatures;

using Model.Interfaces;

namespace Model.Tasks
{
    /// <summary>
    /// Класс составной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableCollection{ITask}"/>.
    /// Реализует <see cref="ITaskComposite"/> и <see cref="ICloneable"/>.
    /// </remarks>
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
        private ITaskComposite? _parentTask;

        /// <summary>
        /// Метаданные.
        /// </summary>
        private object? _metadata;

        /// <inheritdoc/>
        public ITaskComposite? ParentTask
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
        public int Difficult => Count > 0 ? this.Max(x => x.Difficult) : 0;

        /// <inheritdoc/>
        public int Priority => Count > 0 ? this.Max(y => y.Priority) : 0;

        /// <inheritdoc/>
        public TaskStatus Status => Count > 0 ? this.Min(x => x.Status) : TaskStatus.Planned;

        /// <inheritdoc/>
        public DateTime? Deadline => Count > 0 ? this.Max(x => x.Deadline) : null;

        /// <inheritdoc/>
        public double Progress => Count > 0 ? this.Sum(i => i.Progress) / Count : 0;

        /// <inheritdoc/>
        public TimeSpan PlannedTime => this.Aggregate(TimeSpan.Zero,
            (sum, task) => sum + task.PlannedTime);

        /// <inheritdoc/>
        public TimeSpan SpentTime => this.Aggregate(TimeSpan.Zero,
            (sum, task) => sum + task.SpentTime);

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
                notify.PropertyChanged += Task_PropertyChanged;
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
                notify.PropertyChanged -= Task_PropertyChanged;
            }
            foreach (var propertyName in _changedPropertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        private void Task_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_changedPropertyNames.Contains(e.PropertyName))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
