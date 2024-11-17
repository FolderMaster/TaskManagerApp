namespace Model.Interfaces
{
    public interface ITask
    {
        public IFullCollection<ITask>? ParentTask { get; set; }

        public object? Metadata { get; set; }

        public int Difficult { get; }

        public int Priority { get; }

        public TaskStatus Status { get; }

        public DateTime? Deadline { get; }

        public IReadonlyRangeValue<double> Progress { get; }

        public IReadonlyRangeValue<TimeSpan> Time { get; }
    }
}
