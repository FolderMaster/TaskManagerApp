using Model.Tasks.Times;

namespace Model.Interfaces
{
    public interface ITaskElement : ITask
    {
        public new int Difficult { get; set; }

        public new int Priority { get; set; }

        public new TaskStatus Status { get; set; }

        public new DateTime? Deadline { get; set; }

        public new IRangeValue<double> Progress { get; }

        public new IMaxRangeValue<TimeSpan> Time { get; }

        public IMaxRangeValue<double> Real { get; }

        public ITimeIntervalList TimeIntervals { get; }
    }
}
