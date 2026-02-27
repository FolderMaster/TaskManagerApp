using Model.Tasks;

using Database.Entities;

namespace Database.Domains
{
    /// <summary>
    /// Класс домменной модели элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TaskElement"/>.
    /// Реализует <see cref="IDomain"/>.
    /// </remarks>
    public class TaskElementDomain : TaskElement, IDomain
    {
        /// <summary>
        /// Возвращает и задаёт связанную сущность.
        /// </summary>
        public TaskElementEntity Entity { get; set; }

        /// <inheritdoc/>
        public object EntityId => Entity.Task;

        /// <inheritdoc/>
        public override object Clone()
        {
            var result = new TaskElementDomain()
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                PlannedTime = PlannedTime,
                PlannedReal = PlannedReal,
                Entity = new()
                {
                    Task = new()
                }
            };
            if (Metadata is ICloneable cloneable)
            {
                result.Metadata = cloneable.Clone();
            }
            result.Entity.Task.TaskElement = result.Entity;
            return result;
        }
    }
}
