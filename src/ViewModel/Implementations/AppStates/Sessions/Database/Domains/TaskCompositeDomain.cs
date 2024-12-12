using Model.Interfaces;
using Model.Tasks;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    public class TaskCompositeDomain : TaskComposite, IDomain
    {
        public TaskCompositeEntity Entity { get; set; }

        public object EntityId => Entity.Task;

        public TaskCompositeDomain(IEnumerable<ITask>? subtasks = null) : base(subtasks) { }

        public TaskCompositeDomain() : base() { }

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
