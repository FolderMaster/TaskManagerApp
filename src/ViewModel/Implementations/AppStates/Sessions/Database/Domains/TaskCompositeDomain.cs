using Model.Interfaces;
using Model.Tasks;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    /// <summary>
    /// Класс домменной модели составной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="TaskComposite"/>.
    /// Реализует <see cref="IDomain"/>.
    /// </remarks>
    public class TaskCompositeDomain : TaskComposite, IDomain
    {
        /// <summary>
        /// Возвращает и задаёт связанную сущность.
        /// </summary>
        public TaskCompositeEntity Entity { get; set; }

        /// <inheritdoc/>
        public object EntityId => Entity.Task;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskCompositeDomain"/>.
        /// </summary>
        /// <param name="subtasks">Подзадачи.</param>
        public TaskCompositeDomain(IEnumerable<ITask>? subtasks = null) : base(subtasks) { }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskCompositeDomain"/> по умолчанию.
        /// </summary>
        public TaskCompositeDomain() : base() { }

        /// <inheritdoc/>
        public override object Clone()
        {
            var copyList = new List<ITask>();
            foreach (var task in this)
            {
                if (task is ICloneable taskCloneable)
                {
                    copyList.Add((ITask)taskCloneable.Clone());
                }
            }
            var result = new TaskCompositeDomain(copyList);
            if (Metadata is ICloneable metadataCloneable)
            {
                result.Metadata = metadataCloneable.Clone();
            }
            result.Entity = new()
            {
                Task = new()
            };
            result.Entity.Task.TaskComposite = result.Entity;
            return result;
        }
    }
}
