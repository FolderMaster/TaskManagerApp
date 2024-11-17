using Model.Interfaces;
using Model.Technicals;

namespace Model.Tasks.Ranges
{
    public class MaxRangeValue<T> : TrackableObject, IMaxRangeValue<T>
    {
        private T _value;

        private T _max;

        public T Value
        {
            get => _value;
            set => UpdateProperty(ref _value, value);
        }

        public T Max
        {
            get => _max;
            set => UpdateProperty(ref _max, value);
        }

        public T Min { get; private set; }

        public MaxRangeValue(T value, T min, T max)
        {
            _value = value;
            Min = min;
            _max = max;
        }
    }
}
