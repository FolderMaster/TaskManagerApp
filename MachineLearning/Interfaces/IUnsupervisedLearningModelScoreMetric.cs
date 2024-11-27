namespace MachineLearning.Interfaces
{
    public interface IUnsupervisedLearningModelScoreMetric<T, D> : ILearningModelScoreMetric<T>
    {
        public double GetScore(IEnumerable<T> predicted, IEnumerable<IEnumerable<D>> data);
    }
}
