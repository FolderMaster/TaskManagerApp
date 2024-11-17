namespace Model.Interfaces
{
    public interface IRangeValue<T> : IReadonlyRangeValue<T>
    {
        public new T Value { get; set; }
    }
}
