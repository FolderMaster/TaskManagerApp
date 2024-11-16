namespace Model
{
    public class TaskElement : TrackableObject, ITaskElement, ICloneable
    {
        private IFullCollection<ITask>? _parentTask;

        private int _difficult;

        private int _priority;

        private DateTime? _deadline;

        private TaskStatus _status = TaskStatus.Planned;

        private double _progress;

        private TimeSpan _plannedTime;

        private TimeSpan _spentTime;

        private double _totalReal;

        private double _executedReal;

        private object _metadata;

        public IFullCollection<ITask>? ParentTask
        {
            get => _parentTask;
            set => UpdateProperty(ref _parentTask, value);
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

        public double TotalReal
        {
            get => _totalReal;
            set => UpdateProperty(ref _totalReal, value);
        }

        public double ExecutedReal
        {
            get => _executedReal;
            set => UpdateProperty(ref _executedReal, value);
        }

        public object Metadata
        {
            get => _metadata;
            set => UpdateProperty(ref _metadata, value);
        }

        public TimeIntervalCollection TimeIntervals { get; private set; } = new();

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
                TotalReal = TotalReal,
                ExecutedReal = ExecutedReal,
            };
            if (Metadata is ICloneable cloneable)
            {
                result.Metadata = cloneable.Clone();
            }
            return result;
        }
    }
}
