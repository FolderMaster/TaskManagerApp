namespace Model.Interfaces
{
    public interface IMaxRangeValue<T> : IRangeValue<T>
    {
        public new T Max { get; set; }
    }
}
