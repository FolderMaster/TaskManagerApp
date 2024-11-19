using Model.Interfaces;
using Model.Tasks.Times;
using Model.Technicals;

namespace Model.Tasks
{
    public class TaskElement : TrackableObject, ITaskElement, ICloneable
    {
        private IList<ITask>? _parentTask;

        private object? _metadata;

        private int _difficult;

        private int _priority;

        private DateTime? _deadline;

        private TaskStatus _status = TaskStatus.Planned;

        private double _progress;

        private TimeSpan _plannedTime;

        private TimeSpan _spentTime;

        private double _plannedReal;

        private double _executedReal;

        private readonly TimeIntervalList _timeIntervals = new();

        public IList<ITask>? ParentTask
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

        public ITimeIntervalList TimeIntervals => _timeIntervals;

        public double Progress
        {
            get => _progress;
            set => UpdateProperty(ref _progress, value);
        }

        public TimeSpan PlannedTime
        {
            get => _plannedTime;
            set => UpdateProperty(ref _plannedTime, value);
        }

        public TimeSpan SpentTime
        {
            get => _spentTime;
            set => UpdateProperty(ref _spentTime, value);
        }

        public double PlannedReal
        {
            get => _plannedReal;
            set => UpdateProperty(ref _plannedReal, value);
        }

        public double ExecutedReal
        {
            get => _executedReal;
            set => UpdateProperty(ref _executedReal, value);
        }

        public object Clone()
        {
            var result = new TaskElement()
            {
                Difficult = Difficult,
                Priority = Priority,
                Deadline = Deadline,
                Status = Status,
                Progress = Progress,
                PlannedTime = PlannedTime,
                SpentTime = SpentTime,
                PlannedReal = PlannedReal,
                ExecutedReal = ExecutedReal
            };
            if (Metadata is ICloneable cloneable)
            {
                result.Metadata = cloneable.Clone();
            }
            return result;
        }
    }
}
