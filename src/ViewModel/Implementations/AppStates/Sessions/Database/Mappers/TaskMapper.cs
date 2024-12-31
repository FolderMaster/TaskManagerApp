using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений задач между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{TimeIntervalEntity, ITimeIntervalElement}"/>.
    /// </remarks>
    public class TaskMapper : IMapper<TaskEntity, ITask>
    {
        /// <summary>
        /// Преобразование значений между сущностью элементарных задач и элементарными задачами.
        /// </summary>
        private readonly IMapper<TaskElementEntity, ITaskElement> _taskElementMapper;

        /// <summary>
        /// Преобразование значений между сущностью составных задач и составными задачами.
        /// </summary>
        private readonly IMapper<TaskCompositeEntity, ITaskComposite> _taskCompositeMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskMapper"/>.
        /// </summary>
        /// <param name="taskElementMapper">
        /// Преобразование значений между сущностью элементарных задач и элементарными задачами.
        /// </param>
        /// <param name="taskCompositeMapper">
        /// Преобразование значений между сущностью составных задач и составными задачами.
        /// </param>
        public TaskMapper(IMapper<TaskElementEntity, ITaskElement> taskElementMapper,
            IMapper<TaskCompositeEntity, ITaskComposite> taskCompositeMapper)
        {
            _taskElementMapper = taskElementMapper;
            _taskCompositeMapper = taskCompositeMapper;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
