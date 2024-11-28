namespace MachineLearning.Interfaces.Generals
{
    public interface IDataProcessor<T, R>
    {
        public IEnumerable<R> Process(IEnumerable<T> data);
    }
}
