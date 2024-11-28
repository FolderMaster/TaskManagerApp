namespace MachineLearning.Interfaces.Generals
{
    public interface ISupervisedLearningModel<T, R> : ILearningModel<T, R>
    {
        public Task Train(IEnumerable<IEnumerable<T>> values, IEnumerable<R> targets);
    }
}
