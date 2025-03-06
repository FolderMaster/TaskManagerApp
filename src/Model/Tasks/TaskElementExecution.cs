using TrackableFeatures;

using Model.Interfaces;
using Model.Times;

namespace Model.Tasks
{
    /// <summary>
    /// Класс выполнения элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TrackableObject"/>.
    /// Реализует <see cref="ITaskElementExecution"/> и <see cref="ICloneable"/>.
    /// </remarks>
    public class TaskElementExecution : TrackableObject, ITaskElementExecution, ICloneable
    {
        /// <summary>
        /// Список временных интервалов.
        /// </summary>
        private readonly TimeIntervalList _timeIntervals = new();

        /// <summary>
        /// Дата создания.
        /// </summary>
        private readonly DateTime _createdDate;

        /// <summary>
        /// Прогресс.
        /// </summary>
        private double _progress;

        /// <summary>
        /// Статус.
        /// </summary>
        private TaskStatus _status;

        /// <summary>
        /// Потраченное время.
        /// </summary>
        private TimeSpan _spentTime;

        /// <summary>
        /// Выполненный реальный показатель.
        /// </summary>
        private double _executedReal;

        /// <inheritdoc/>
        public double Progress
        {
            get => _progress;
            set => UpdateProperty(ref _progress, value);
        }

        /// <inheritdoc/>
        public TaskStatus Status
        {
            get => _status;
            set => UpdateProperty(ref _status, value);
        }

        /// <inheritdoc/>
        public TimeSpan SpentTime
        {
            get => _spentTime;
            set => UpdateProperty(ref _spentTime, value);
        }

        /// <inheritdoc/>
        public double ExecutedReal
        {
            get => _executedReal;
            set => UpdateProperty(ref _executedReal, value);
        }

        /// <inheritdoc/>
        public ITimeIntervalList TimeIntervals => _timeIntervals;

        /// <inheritdoc/>
        public DateTime CreatedDate => _createdDate;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementExecution"/>.
        /// </summary>
        /// <param name="createdDate">Дата создания.</param>
        public TaskElementExecution(DateTime? createdDate = null)
        {
            _createdDate = createdDate ?? DateTime.Now;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementExecution"/> по умолчанию.
        /// </summary>
        public TaskElementExecution() : this(null) { }

        /// <inheritdoc/>
        public object Clone()
        {
            var result = new TaskElementExecution()
            {
                Progress = Progress,
                Status = Status,
                SpentTime = SpentTime,
                ExecutedReal = ExecutedReal
            };
            return result;
        }
    }
}
