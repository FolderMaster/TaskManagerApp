namespace ViewModel.Interfaces.ModelLearning
{
    public interface IDataTransformer<T, R>
    {
        IEnumerable<R> FitTransform(IEnumerable<T> data);

        R Transform(T data);
    }
}
