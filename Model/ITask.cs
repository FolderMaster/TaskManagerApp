namespace Model
{
    public interface ITask
    {
        public IFullCollection<ITask>? ParentTask { get; set; }

        public int Difficult { get; }

        public int Priority { get; }

        public TaskStatus Status { get; }

        public DateTime? Deadline { get; }

        public object Metadata { get; set; }

        public double Progress { get; }

        public TimeSpan PlannedTime { get; }

        public TimeSpan SpentTime { get; }
    }
}
