namespace MachineLearning.Interfaces.Generals
{
    public interface IUnsupervisedScoreMetric<T, D> : IScoreMetric<T>
    {
        public double GetScore(IEnumerable<T> predicted, IEnumerable<IEnumerable<D>> data);
    }
}
