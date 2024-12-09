using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    public class TaskCompositeMapper : IMapper<TaskCompositeEntity, ITaskComposite>
    {
        private readonly IMapper<MetadataEntity, object> _metadataConverter;

        private readonly IMapper<TaskElementEntity, ITaskElement> _taskElementConverter;

        public TaskCompositeMapper(IMapper<MetadataEntity, object> metadataConverter,
            IMapper<TaskElementEntity, ITaskElement> taskElementConverter)
        {
            _metadataConverter = metadataConverter;
            _taskElementConverter = taskElementConverter;
        }

        public ITaskComposite Map(TaskCompositeEntity value)
        {
            var result = new TaskCompositeDomain()
            {
                Entity = value,
                Metadata = _metadataConverter.Map(value.Task.Metadata)
            };
            foreach (var task in value.Subtasks)
            {
                var addingTask = (ITask?)null;
                if (task.TaskElement != null)
                {
                    addingTask = _taskElementConverter.Map(task.TaskElement);
                }
                else if (task.TaskComposite != null)
                {
                    addingTask = Map(task.TaskComposite);
                }
                else
                {
                    throw new InvalidOperationException();
                }
                result.Add(addingTask);
            }
            return result;
        }

        public TaskCompositeEntity MapBack(ITaskComposite value)
        {
            if (value is not TaskCompositeDomain domain)
            {
                throw new ArgumentException(nameof(value));
            }
            if (domain.Metadata == null)
            {
                throw new ArgumentException(nameof(value));
            }
            var result = domain.Entity;
            var parentTask = domain.ParentTask as TaskCompositeDomain;
            result.Task.ParentTask = parentTask?.Entity;
            result.Task.Metadata = _metadataConverter.MapBack(domain.Metadata);
            foreach (var subtask in value)
            {
                var addingTask = (TaskEntity?)null;
                switch (subtask)
                {
                    case TaskElementDomain elementDomain:
                        var taskElement =
                            _taskElementConverter.MapBack(elementDomain);
                        addingTask = taskElement.Task;
                        break;
                    case TaskCompositeDomain compositeDomain:
                        var taskComposite = MapBack(compositeDomain);
                        addingTask = taskComposite.Task;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                result.Subtasks.Add(addingTask);
            }
            return result;
        }
    }
}
