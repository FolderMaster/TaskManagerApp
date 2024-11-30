using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Interfaces
{
    public interface IPointDataProcessor<T, R> :
        IDataProcessor<IEnumerable<T>, IEnumerable<R>> { }
}
