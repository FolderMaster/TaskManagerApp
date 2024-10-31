namespace Model
{
    public class TimeInterval : ObservableObject, ITimeInterval
    {
        private DateTime _start;

        private DateTime _end;

        public DateTime Start
        {
            get => _start;
            set => OnPropertyChanged(ref _start, value, () => OnPropertyChanged(nameof(Duration)));
        }

        public DateTime End
        {
            get => _end;
            set => OnPropertyChanged(ref _end, value, () => OnPropertyChanged(nameof(Duration)));
        }

        public TimeSpan Duration => End - Start;

        public TimeInterval(DateTime? start = null, DateTime? end = null)
        {
            Start = start ?? DateTime.Now;
            End = end ?? DateTime.Now;
        }
    }
}
