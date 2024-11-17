using Model.Interfaces;
using Model.Tasks.Ranges;
using Model.Tasks.Times;
using Model.Technicals;

namespace Model.Tasks
{
    public class TaskElement : TrackableObject, ITaskElement, ICloneable
    {
        private IFullCollection<ITask>? _parentTask;

        private object? _metadata;

        private int _difficult;

        private int _priority;

        private DateTime? _deadline;

        private TaskStatus _status = TaskStatus.Planned;

        private readonly MaxRangeValue<TimeSpan> _time =
            new(TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero);

        private readonly RangeValue<double> _progress = new(0, 0, 100);

        private readonly MaxRangeValue<double> _real = new(0, 0, 0);

        private readonly TimeIntervalList _timeIntervals = new();

        public IFullCollection<ITask>? ParentTask
        {
            get => _parentTask;
            set => UpdateProperty(ref _parentTask, value);
        }

        public object? Metadata
        {
            get => _metadata;
            set => UpdateProperty(ref _metadata, value);
        }

        public int Difficult
        {
            get => _difficult;
            set => UpdateProperty(ref _difficult, value);
        }

        public int Priority
        {
            get => _priority;
            set => UpdateProperty(ref _priority, value);
        }

        public DateTime? Deadline
        {
            get => _deadline;
            set => UpdateProperty(ref _deadline, value);
        }

        public TaskStatus Status
        {
            get => _status;
            set => UpdateProperty(ref _status, value);
        }

        public IRangeValue<double> Progress => _progress;

        public IMaxRangeValue<TimeSpan> Time => _time;

        public IMaxRangeValue<double> Real => _real;

        public ITimeIntervalList TimeIntervals => _timeIntervals;

        IReadonlyRangeValue<double> ITask.Progress => _progress;

        IReadonlyRangeValue<TimeSpan> ITask.Time => _time;

        public object Clone()
        {
            var result = new TaskElement()
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                Status = Status
            };
            result.Progress.Value = Progress.Value;
            result.Time.Value = Time.Value;
            result.Time.Max = Time.Max;
            result.Real.Value = Real.Value;
            result.Real.Max = Real.Max;
            if (Metadata is ICloneable cloneable)
            {
                result.Metadata = cloneable.Clone();
            }
            return result;
        }
    }
}
