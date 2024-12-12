using Model.Tasks;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    public class TaskElementDomain : TaskElement, IDomain
    {
        public TaskElementEntity Entity { get; set; }

        public object EntityId => Entity.Task;

        public override object Clone()
        {
            var result = new TaskElementDomain()
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                Status = Status,
                Progress = Progress,
                PlannedTime = PlannedTime,
                SpentTime = SpentTime,
                PlannedReal = PlannedReal,
                ExecutedReal = ExecutedReal,
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
