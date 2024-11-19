namespace Model.Interfaces
{
    public interface ITask
    {
        public IList<ITask>? ParentTask { get; set; }

        public object? Metadata { get; set; }

        public int Difficult { get; }

        public int Priority { get; }

        public TaskStatus Status { get; }

        public DateTime? Deadline { get; }

        public double Progress { get; }

        public TimeSpan PlannedTime { get; }

        public TimeSpan SpentTime { get; }
    }
}
