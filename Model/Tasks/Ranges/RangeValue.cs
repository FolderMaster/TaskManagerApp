using Model.Interfaces;
using Model.Technicals;

namespace Model.Tasks.Ranges
{
    public class RangeValue<T> : TrackableObject, IRangeValue<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set => UpdateProperty(ref _value, value);
        }

        public T Max { get; private set; }

        public T Min { get; private set; }

        public RangeValue(T value, T min, T max)
        {
            _value = value;
            Min = min;
            Max = max;
        }
    }
}
