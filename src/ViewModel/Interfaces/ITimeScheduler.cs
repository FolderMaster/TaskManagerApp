namespace ViewModel.Interfaces
{
    public interface ITimeScheduler
    {
        public IList<DateTime> Timepoints { get; }

        public event EventHandler<DateTime> TimepointReached;
    }
}
