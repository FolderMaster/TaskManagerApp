namespace MachineLearning.Interfaces.Generals
{
    public interface IUnsupervisedScoreMetric<T, D> : IScoreMetric<T>
    {
        public double CalculateScore(IEnumerable<T> actual, IEnumerable<IEnumerable<D>> data);
    }
}
