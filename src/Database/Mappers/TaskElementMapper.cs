using Model.Interfaces;

using Database.Domains;
using Database.Entities;

namespace Database.Mappers
{
    /// <summary>
    /// Класс перобразования значений элементарных задач между двумя предметными областями.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IMapper{TimeIntervalEntity, ITimeIntervalElement}"/>.
    /// </remarks>
    public class TaskElementMapper : IMapper<TaskElementEntity, ITaskElement>
    {
        /// <summary>
        /// Преобразование значений между сущностью метаданных и метаданными.
        /// </summary>
        private readonly IMapper<MetadataEntity, object> _metadataMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementMapper"/>.
        /// </summary>
        /// <param name="metadataMapper">
        /// Преобразование значений между сущностью метаданных и метаданными.
        /// </param>
        public TaskElementMapper(IMapper<MetadataEntity, object> metadataMapper)
        {
            _metadataMapper = metadataMapper;
        }

        /// <inheritdoc/>
        public ITaskElement Map(TaskElementEntity value)
        {
            var result = new TaskElementDomain()
            {
                Entity = value,
                Difficult = value.Difficult,
                Priority = value.Priority,
                Deadline = value.Deadline,
                PlannedTime = value.PlannedTime,
                PlannedReal = value.PlannedReal
            };
            result.Metadata = _metadataMapper.Map(value.Task.Metadata);
            return result;
        }

        /// <inheritdoc/>
        public TaskElementEntity MapBack(ITaskElement value)
        {
            if (value is not TaskElementDomain domain)
            {
                throw new ArgumentException(nameof(value));
            }
            if (domain.Metadata == null)
            {
                throw new ArgumentException(nameof(value));
            }
            var result = domain.Entity;
            result.Difficult = domain.Difficult;
            result.Priority = domain.Priority;
            result.Deadline = domain.Deadline;
            result.PlannedTime = domain.PlannedTime;
            result.PlannedReal = domain.PlannedReal;
            var parentTask = domain.ParentTask as TaskCompositeDomain;
            result.Task.ParentTask = parentTask?.Entity;
            result.Task.Metadata = _metadataMapper.MapBack(domain.Metadata);
            return result;
        }
    }
}
