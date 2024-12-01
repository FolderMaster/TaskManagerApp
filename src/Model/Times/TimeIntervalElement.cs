using TrackableFeatures;

using Model.Interfaces;

namespace Model.Times
{
    public class TimeIntervalElement : TrackableObject, ITimeIntervalElement
    {
        private DateTime _start;

        private DateTime _end;

        public DateTime Start
        {
            get => _start;
            set => UpdateProperty(ref _start, value, () => OnPropertyChanged(nameof(Duration)));
        }

        public DateTime End
        {
            get => _end;
            set => UpdateProperty(ref _end, value, () => OnPropertyChanged(nameof(Duration)));
        }

        public TimeSpan Duration => End - Start;

        public TimeIntervalElement(DateTime? start = null, DateTime? end = null)
        {
            Start = start ?? DateTime.Now;
            End = end ?? DateTime.Now;
        }

        public TimeIntervalElement() : this(null, null) { }
    }
}
