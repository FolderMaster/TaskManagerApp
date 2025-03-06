using System.Collections.ObjectModel;
using System.ComponentModel;

using Model.Interfaces;

namespace Model.Tasks
{
    /// <summary>
    /// Класс повторяющейся элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElement"/>.
    /// Реализует <see cref="IRecurringTaskElement"/> и <see cref="ICloneable"/>.
    /// </remarks>
    public class RecurringTaskElement : BaseTaskElement,
        IRecurringTaskElement, ICloneable
    {
        /// <summary>
        /// Названия изменящихся свойств.
        /// </summary>
        private static readonly IEnumerable<string> _changedPropertyNames =
            [nameof(Status), nameof(Progress), nameof(SpentTime), nameof(ExecutedReal)];

        /// <summary>
        /// Настройка повторения.
        /// </summary>
        private readonly RecurringSettings _recurringSettings = new();

        /// <summary>
        /// Последняя дата обновления выполнений.
        /// </summary>
        private DateTime _lastUpdatedExecutionsDate;

        /// <summary>
        /// Выполнения элементарной задачи.
        /// </summary>
        protected readonly ObservableCollection<ITaskElementExecution> _executions = new();

        /// <inheritdoc/>
        public RecurringSettings RecurringSettings => _recurringSettings;

        /// <inheritdoc/>
        public DateTime LastUpdatedExecutionsDate => _lastUpdatedExecutionsDate;

        /// <inheritdoc/>
        public override TaskStatus Status => _executions.Count > 0 ?
            _executions.Min(x => x.Status) : TaskStatus.Planned;

        /// <inheritdoc/>
        public override double Progress => _executions.Count > 0 ?
            _executions.Sum(i => i.Progress) / _executions.Count : 0;

        /// <inheritdoc/>
        public override TimeSpan SpentTime => _executions.Aggregate(TimeSpan.Zero,
            (sum, execution) => sum + execution.SpentTime);

        /// <inheritdoc/>
        public override double ExecutedReal => _executions.Aggregate(0d,
            (sum, execution) => sum + execution.ExecutedReal);

        /// <inheritdoc/>
        public override IEnumerable<ITaskElementExecution> Executions => _executions;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringTaskElement"/>.
        /// </summary>
        /// <param name="executions">Выполнения элементарной задачи.</param>
        /// <param name="lastUpdatedExecutionsDate">Последняя дата обновления выполнений.</param>
        public RecurringTaskElement(IEnumerable<ITaskElementExecution>? executions = null,
            DateTime? lastUpdatedExecutionsDate = null) 
        {
            _executions = executions != null ? new(executions) : new();
            RecurringSettings.StartDate = DateTime.Now;
            _lastUpdatedExecutionsDate = lastUpdatedExecutionsDate ?? DateTime.Now;
            foreach (var execution in _executions)
            {
                if (execution is INotifyPropertyChanged notify)
                {
                    notify.PropertyChanged += Execution_PropertyChanged;
                }
            }
            UpdateExecutions();
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringTaskElement"/> по умолчанию.
        /// </summary>
        public RecurringTaskElement() : this(null) { }

        /// <inheritdoc/>
        public void UpdateExecutions()
        {
            var now = DateTime.Now;
            var occurrences = RecurringSettings.CalculateOccurrences(_lastUpdatedExecutionsDate, now);
            foreach (var occurrence in occurrences)
            {
                _executions.Add(new TaskElementExecution(occurrence));
            }
            _lastUpdatedExecutionsDate = now;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            var executions = new List<ITaskElementExecution>();
            foreach (var execution in _executions)
            {
                if (execution is ICloneable executionCloneable)
                {
                    executions.Add((ITaskElementExecution)executionCloneable.Clone());
                }
            }
            var result = new RecurringTaskElement(executions)
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                PlannedTime = PlannedTime,
                PlannedReal = PlannedReal
            };
            if (Metadata is ICloneable metadataCloneable)
            {
                result.Metadata = metadataCloneable.Clone();
            }
            return result;
        }

        private void Execution_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_changedPropertyNames.Contains(e.PropertyName))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
