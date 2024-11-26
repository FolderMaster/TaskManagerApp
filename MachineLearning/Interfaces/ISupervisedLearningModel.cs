namespace MachineLearning.Interfaces
{
    public interface ISupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        public Task Train(IEnumerable<IEnumerable<T>> values, IEnumerable<R> targets);
    }
}
