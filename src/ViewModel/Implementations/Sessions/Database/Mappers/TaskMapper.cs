using Model.Interfaces;

using ViewModel.Implementations.Sessions.Database.Domains;
using ViewModel.Implementations.Sessions.Database.Entities;

namespace ViewModel.Implementations.Sessions.Database.Mappers
{
    public class TaskMapper : IMapper<TaskEntity, ITask>
    {
        private readonly IMapper<TaskElementEntity, ITaskElement> _taskElementMapper;

        private readonly IMapper<TaskCompositeEntity, ITaskComposite> _taskCompositeMapper;

        public TaskMapper(IMapper<TaskElementEntity, ITaskElement> taskElementMapper,
            IMapper<TaskCompositeEntity, ITaskComposite> taskCompositeMapper)
        {
            _taskElementMapper = taskElementMapper;
            _taskCompositeMapper = taskCompositeMapper;
        }

        public ITask Map(TaskEntity value)
        {
            if (value.TaskElement != null)
            {
                return _taskElementMapper.Map(value.TaskElement);
            }
            else if (value.TaskComposite != null)
            {
                return _taskCompositeMapper.Map(value.TaskComposite);
            }
            throw new ArgumentException(nameof(value));
        }

        public TaskEntity MapBack(ITask value)
        {
            if (value is TaskElementDomain elementDomain)
            {
                var elementEntity = _taskElementMapper.MapBack(elementDomain);
                return elementEntity.Task;
            }
            else if (value is TaskCompositeDomain compositeDomain)
            {
                var compositeEntity = _taskCompositeMapper.MapBack(compositeDomain);
                return compositeEntity.Task;
            }
            throw new ArgumentException(nameof(value));
        }
    }
}
