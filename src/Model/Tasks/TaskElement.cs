using System.ComponentModel;

using Model.Interfaces;

namespace Model.Tasks
{
    /// <summary>
    /// Класс элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElement"/>.
    /// Реализует <see cref="ICloneable"/>.
    /// </remarks>
    public class TaskElement : BaseTaskElement, ICloneable
    {
        /// <summary>
        /// Названия изменящихся свойств.
        /// </summary>
        private static readonly IEnumerable<string> _changedPropertyNames =
            [ nameof(Status), nameof(Progress), nameof(SpentTime), nameof(ExecutedReal) ];

        /// <summary>
        /// Выполнение элементарной задачи.
        /// </summary>
        protected readonly ITaskElementExecution _execution;

        /// <inheritdoc/>
        public override TaskStatus Status => _execution.Status;

        /// <inheritdoc/>
        public override double Progress => _execution.Progress;

        /// <inheritdoc/>
        public override TimeSpan SpentTime => _execution.SpentTime;

        /// <inheritdoc/>
        public override double ExecutedReal => _execution.ExecutedReal;

        /// <inheritdoc/>
        public override IEnumerable<ITaskElementExecution> Executions => [ _execution ];

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElement"/>.
        /// </summary>
        /// <param name="execution">Выполнение элементарной задачи.</param>
        public TaskElement(ITaskElementExecution? execution = null)
        {
            _execution = execution ?? new TaskElementExecution();
            if (execution is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += Execution_PropertyChanged;
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElement"/> по умолчанию.
        /// </summary>
        public TaskElement() : this(null) { }

        /// <inheritdoc/>
        public object Clone()
        {
            var execution = (ITaskElementExecution?)null;
            if (_execution is ICloneable executionCloneable)
            {
                execution = (ITaskElementExecution)executionCloneable.Clone();
            }
            var result = new TaskElement(execution)
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

        protected void Execution_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_changedPropertyNames.Contains(e.PropertyName))
            {
                OnPropertyChanged(e.PropertyName);
            }
        }
    }
}
