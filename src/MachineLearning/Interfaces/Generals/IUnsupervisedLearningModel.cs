namespace MachineLearning.Interfaces.Generals
{
    public interface IUnsupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        public Task Train(IEnumerable<IEnumerable<T>> values);
    }
}
