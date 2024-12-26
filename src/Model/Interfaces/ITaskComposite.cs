using System.Collections;

namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс составной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITask"/>, <see cref="IList{ITask}"/> и  <see cref="IList"/>.
    /// </remarks>
    public interface ITaskComposite : IList<ITask>, IList, ITask { }
}
