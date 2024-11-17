namespace Model.Interfaces
{
    public interface IReadonlyRangeValue<T>
    {
        public T Value { get; }

        public T Min { get; }

        public T Max { get; }
    }
}
