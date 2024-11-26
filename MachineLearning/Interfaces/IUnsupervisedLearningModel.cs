namespace MachineLearning.Interfaces
{
    public interface IUnsupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        public Task Train(IEnumerable<IEnumerable<T>> values);
    }
}
