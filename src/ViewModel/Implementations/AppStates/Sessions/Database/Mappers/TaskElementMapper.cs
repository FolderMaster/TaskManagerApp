using Model.Interfaces;

using ViewModel.Implementations.AppStates.Sessions.Database.Domains;
using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    public class TaskElementMapper : IMapper<TaskElementEntity, ITaskElement>
    {
        private readonly IMapper<MetadataEntity, object> _metadataConverter;

        private readonly IMapper<TimeIntervalEntity, ITimeIntervalElement>
            _timeIntervalConverter;

        public TaskElementMapper(IMapper<MetadataEntity, object> metadataConverter,
            IMapper<TimeIntervalEntity, ITimeIntervalElement> timeIntervalConverter)
        {
            _metadataConverter = metadataConverter;
            _timeIntervalConverter = timeIntervalConverter;
        }

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
            result.Metadata = _metadataConverter.Map(value.Task.Metadata);
            foreach (var timeInterval in value.TimeIntervals)
            {
                var interval = _timeIntervalConverter.Map(timeInterval);
                result.TimeIntervals.Add(interval);
            }
            return result;
        }

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
            result.Task.Metadata = _metadataConverter.MapBack(domain.Metadata);
            result.TimeIntervals = domain.TimeIntervals.
                Select(_timeIntervalConverter.MapBack).ToList();
            return result;
        }
    }
}
