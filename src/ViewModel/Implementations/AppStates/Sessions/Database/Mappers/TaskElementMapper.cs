using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
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
        /// Преобразование значений между сущностью временных интервалов и
        /// элементарными временными интервалами.
        /// </summary>
        private readonly IMapper<TimeIntervalEntity, ITimeIntervalElement> _timeIntervalMapper;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="TaskElementMapper"/>.
        /// </summary>
        /// <param name="metadataMapper">
        /// Преобразование значений между сущностью метаданных и метаданными.
        /// </param>
        /// <param name="timeIntervalMapper">
        /// Преобразование значений между сущностью временных интервалов и
        /// элементарными временными интервалами.
        /// </param>
        public TaskElementMapper(IMapper<MetadataEntity, object> metadataMapper,
            IMapper<TimeIntervalEntity, ITimeIntervalElement> timeIntervalMapper)
        {
            _metadataMapper = metadataMapper;
            _timeIntervalMapper = timeIntervalMapper;
        }

        /// <inheritdoc/>
        public ITaskElement Map(TaskElementEntity value)
        {
            var result = new TaskElementDomain()
            {
                Entity = value,
                Difficult = value.Difficult,
                Priority = value.Priority,
                Status = value.Status,
                Progress = value.Progress,
                Deadline = value.Deadline,
                PlannedTime = value.PlannedTime,
                SpentTime = value.SpentTime,
                ExecutedReal = value.ExecutedReal,
                PlannedReal = value.PlannedReal
            };
            result.Metadata = _metadataMapper.Map(value.Task.Metadata);
            foreach (var timeInterval in value.TimeIntervals)
            {
                var interval = _timeIntervalMapper.Map(timeInterval);
                result.TimeIntervals.Add(interval);
            }
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
            result.Status = domain.Status;
            result.Progress = domain.Progress;
            result.Deadline = domain.Deadline;
            result.PlannedTime = domain.PlannedTime;
            result.SpentTime = domain.SpentTime;
            result.ExecutedReal = domain.ExecutedReal;
            result.PlannedReal = domain.PlannedReal;
            var parentTask = domain.ParentTask as TaskCompositeDomain;
            result.Task.ParentTask = parentTask?.Entity;
            result.Task.Metadata = _metadataMapper.MapBack(domain.Metadata);
            result.TimeIntervals = domain.TimeIntervals.
                Select(_timeIntervalMapper.MapBack).ToList();
            return result;
        }
    }
}
