namespace MachineLearning.Interfaces
{
    public interface ISupervisedLearningModelScoreMetric<T> : ILearningModelScoreMetric<T>
    {
        public double CalculateScore(IEnumerable<T> expected, IEnumerable<T> predicted);
    }
}
