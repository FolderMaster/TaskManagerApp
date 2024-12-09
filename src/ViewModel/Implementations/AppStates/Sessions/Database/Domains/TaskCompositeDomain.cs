using Model.Tasks;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    public class TaskCompositeDomain : TaskComposite, IDomain
    {
        public TaskCompositeEntity Entity { get; set; }

        public object EntityId => Entity.Task;
    }
}
