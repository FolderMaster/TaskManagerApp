using TrackableFeatures;

using Model.Interfaces;

namespace Model.Tasks
{
    /// <summary>
    /// Базовый абстрактный класс элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ITaskElement"/>.
    /// </remarks>
    public abstract class BaseTaskElement : TrackableObject, ITaskElement
    {
        /// <summary>
        /// Родительская задача.
        /// </summary>
        private ITaskComposite? _parentTask;

        /// <summary>
        /// Метаданные.
        /// </summary>
        private object? _metadata;

        /// <summary>
        /// Сложность.
        /// </summary>
        private int _difficult;

        /// <summary>
        /// Приоритет.
        /// </summary>
        private int _priority;

        /// <summary>
        /// Срок.
        /// </summary>
        private DateTime? _deadline;

        /// <summary>
        /// Запланированное время.
        /// </summary>
        private TimeSpan _plannedTime;

        /// <summary>
        /// Запланированный реальный показатель.
        /// </summary>
        private double _plannedReal;

        /// <inheritdoc/>
        public abstract TaskStatus Status { get; }

        /// <inheritdoc/>
        public abstract double Progress { get; }

        /// <inheritdoc/>
        public abstract TimeSpan SpentTime { get; }

        /// <inheritdoc/>
        public abstract double ExecutedReal { get; }

        /// <inheritdoc/>
        public abstract IEnumerable<ITaskElementExecution> Executions { get; }

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
        public int Difficult
        {
            get => _difficult;
            set => UpdateProperty(ref _difficult, value);
        }

        /// <inheritdoc/>
        public int Priority
        {
            get => _priority;
            set => UpdateProperty(ref _priority, value);
        }

        /// <inheritdoc/>
        public DateTime? Deadline
        {
            get => _deadline;
            set => UpdateProperty(ref _deadline, value);
        }

        /// <inheritdoc/>
        public TimeSpan PlannedTime
        {
            get => _plannedTime;
            set => UpdateProperty(ref _plannedTime, value);
        }

        /// <inheritdoc/>
        public double PlannedReal
        {
            get => _plannedReal;
            set => UpdateProperty(ref _plannedReal, value);
        }
    }
}
