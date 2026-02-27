using Model.Interfaces;

using Database.Entities;
using Model.Tasks.RecurringTasks;

namespace Database.Domains
{
    /// <summary>
    /// Класс домменной модели повторящейся элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="RecurringTaskElement"/>.
    /// Реализует <see cref="IDomain"/>.
    /// </remarks>
    public class RecurringTaskElementDomain : RecurringTaskElement, IDomain
    {
        /// <summary>
        /// Возвращает и задаёт связанную сущность.
        /// </summary>
        public RecurringTaskElementEntity Entity { get; set; }

        /// <inheritdoc/>
        public object? EntityId => Entity.TaskElement.Task;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringTaskElementDomain"/>.
        /// </summary>
        /// <param name="executions">Выполнения элементарной задачи.</param>
        /// <param name="lastUpdatedExecutionsDate">Последняя дата обновления выполнений.</param>
        public RecurringTaskElementDomain(IEnumerable<ITaskElementExecution>? executions = null,
            DateTime? lastUpdatedExecutionsDate = null) :
            base(executions, lastUpdatedExecutionsDate) { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="RecurringTaskElementDomain"/> по умолчанию.
        /// </summary>
        public RecurringTaskElementDomain() : this(null) { }

        /// <inheritdoc/>
        public override object Clone()
        {

        }
    }
}
