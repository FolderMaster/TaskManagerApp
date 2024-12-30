using TrackableFeatures;

using Model.Interfaces;
using Model.Times;

namespace Model.Tasks
{
    /// <summary>
    /// Класс элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ITaskElement"/> и <see cref="ICloneable"/>.
    /// </remarks>
    public class TaskElement : TrackableObject, ITaskElement, ICloneable
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
        /// Статус.
        /// </summary>
        private TaskStatus _status = TaskStatus.Planned;

        /// <summary>
        /// Прогресс.
        /// </summary>
        private double _progress;

        /// <summary>
        /// Запланированное время.
        /// </summary>
        private TimeSpan _plannedTime;

        /// <summary>
        /// Потраченное время.
        /// </summary>
        private TimeSpan _spentTime;

        /// <summary>
        /// Запланированный реальный показатель.
        /// </summary>
        private double _plannedReal;

        /// <summary>
        /// Выполненный реальный показатель.
        /// </summary>
        private double _executedReal;

        /// <summary>
        /// Список временных интервалов.
        /// </summary>
        private readonly TimeIntervalList _timeIntervals = new();

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
        public TaskStatus Status
        {
            get => _status;
            set => UpdateProperty(ref _status, value);
        }

        /// <inheritdoc/>
        public ITimeIntervalList TimeIntervals => _timeIntervals;

        /// <inheritdoc/>
        public double Progress
        {
            get => _progress;
            set => UpdateProperty(ref _progress, value);
        }

        /// <inheritdoc/>
        public TimeSpan PlannedTime
        {
            get => _plannedTime;
            set => UpdateProperty(ref _plannedTime, value);
        }

        /// <inheritdoc/>
        public TimeSpan SpentTime
        {
            get => _spentTime;
            set => UpdateProperty(ref _spentTime, value);
        }

        /// <inheritdoc/>
        public double PlannedReal
        {
            get => _plannedReal;
            set => UpdateProperty(ref _plannedReal, value);
        }

        /// <inheritdoc/>
        public double ExecutedReal
        {
            get => _executedReal;
            set => UpdateProperty(ref _executedReal, value);
        }

        /// <inheritdoc/>
        public virtual object Clone()
        {
            var result = new TaskElement()
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                Status = Status,
                Progress = Progress,
                PlannedTime = PlannedTime,
                SpentTime = SpentTime,
                PlannedReal = PlannedReal,
                ExecutedReal = ExecutedReal
            };
            if (Metadata is ICloneable cloneable)
            {
                result.Metadata = cloneable.Clone();
            }
            return result;
        }
    }
}
