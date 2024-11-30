namespace MachineLearning.Interfaces.Generals
{
    public interface ISupervisedScoreMetric<T> : IScoreMetric<T>
    {
        public double CalculateScore(IEnumerable<T> expected, IEnumerable<T> predicted);
    }
}
