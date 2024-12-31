using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений составных задач между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{TimeIntervalEntity, ITimeIntervalElement}"/>.
    /// </remarks>
    public class TaskCompositeMapper : IMapper<TaskCompositeEntity, ITaskComposite>
    {
        /// <summary>
        /// Преобразование значений между сущностью метаданных и метаданными.
        /// </summary>
        private readonly IMapper<MetadataEntity, object> _metadataMapper;

        /// <summary>
        /// Преобразование значений между сущностью элементарных задач и элементарными задачами.
        /// </summary>
        private readonly IMapper<TaskElementEntity, ITaskElement> _taskElementMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskCompositeMapper"/>.
        /// </summary>
        /// <param name="metadataMapper">
        /// Преобразование значений между сущностью метаданных и метаданными.
        /// </param>
        /// <param name="taskElementMapper">
        /// Преобразование значений между сущностью элементарных задач и элементарными задачами.
        /// </param>
        public TaskCompositeMapper(IMapper<MetadataEntity, object> metadataMapper,
            IMapper<TaskElementEntity, ITaskElement> taskElementMapper)
        {
            _metadataMapper = metadataMapper;
            _taskElementMapper = taskElementMapper;
        }

        /// <inheritdoc/>
        public ITaskComposite Map(TaskCompositeEntity value)
        {
            var result = new TaskCompositeDomain()
            {
                Entity = value,
                Metadata = _metadataMapper.Map(value.Task.Metadata)
            };
            foreach (var task in value.Subtasks)
            {
                var addingTask = (ITask?)null;
                if (task.TaskElement != null)
                {
                    addingTask = _taskElementMapper.Map(task.TaskElement);
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

        /// <inheritdoc/>
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
            result.Task.Metadata = _metadataMapper.MapBack(domain.Metadata);
            foreach (var subtask in value)
            {
                var addingTask = (TaskEntity?)null;
                switch (subtask)
                {
                    case TaskElementDomain elementDomain:
                        var taskElement =
                            _taskElementMapper.MapBack(elementDomain);
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
