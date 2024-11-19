namespace Model.Interfaces
{
    public interface ITaskElement : ITask
    {
        public new int Difficult { get; set; }

        public new int Priority { get; set; }

        public new TaskStatus Status { get; set; }

        public new DateTime? Deadline { get; set; }

        public new double Progress { get; set; }

        public new TimeSpan PlannedTime { get; set; }

        public new TimeSpan SpentTime { get; set; }

        public double PlannedReal { get; set; }

        public double ExecutedReal { get; set; }

        public ITimeIntervalList TimeIntervals { get; }
    }
}
