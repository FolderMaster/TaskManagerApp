using Model.Tasks;

using ViewModel.Implementations.Sessions.Database.Entities;

namespace ViewModel.Implementations.Sessions.Database.Domains
{
    public class TaskCompositeDomain : TaskComposite, IDomain
    {
        public TaskCompositeEntity Entity { get; set; }

        public object EntityId => Entity.Task;
    }
}
