using Model.Tasks;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    public class TaskElementDomain : TaskElement, IDomain
    {
        public TaskElementEntity Entity { get; set; }

        public object EntityId => Entity.Task;
    }
}
