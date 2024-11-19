using System.Collections;

namespace Model.Interfaces
{
    public interface ITaskComposite : IList<ITask>, IList, ITask { }
}
