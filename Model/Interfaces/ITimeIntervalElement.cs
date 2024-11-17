namespace Model.Interfaces
{
    public interface ITimeIntervalElement : ITimeInterval
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
