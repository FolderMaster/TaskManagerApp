using Model.Tasks;
using Model.Interfaces;

using Database.Entities;
using Model.Tasks.RecurringTasks;

namespace Database.Domains
{
    /// <summary>
    /// Класс домменной модели выполнения элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="RecurringTaskElement"/>.
    /// Реализует <see cref="IDomain"/>.
    /// </remarks>
    public class TaskElementExecutionDomain : TaskElementExecution, IDomain
    {
        /// <summary>
        /// Возвращает и задаёт связанную сущность.
        /// </summary>
        public TaskElementExecutionEntity Entity { get; set; }

        /// <inheritdoc/>
        public object EntityId => Entity;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementExecutionDomain"/>.
        /// </summary>
        /// <param name="taskElement">Элементарная задача.</param>
        /// <param name="createdDate">Дата создания.</param>
        public TaskElementExecutionDomain(ITaskElement? taskElement = null, DateTime?
            createdDate = null) : base(taskElement, createdDate) { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementExecutionDomain"/> по умолчанию.
        /// </summary>
        public TaskElementExecutionDomain() : this(null) { }

        /// <inheritdoc/>
        public override object Clone()
        {
            var result = new TaskElementExecutionDomain()
            {
                Progress = Progress,
                Status = Status,
                SpentTime = SpentTime,
                ExecutedReal = ExecutedReal,
                Entity = new()
            };
            return result;
        }
    }
}
