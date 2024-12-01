using System.Collections;

namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс составной задачи. Наследует <see cref="ITask"/>, <see cref="IList{ITask}"/> и
    /// <see cref="IList"/>.
    /// </summary>
    public interface ITaskComposite : IList<ITask>, IList, ITask { }
}
