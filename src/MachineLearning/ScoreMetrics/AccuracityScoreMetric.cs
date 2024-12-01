using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class AccuracyScoreMetric : IClassificationScoreMetric
    {
        public double CalculateScore(IEnumerable<int> actual, IEnumerable<int> predicted)
        {
            var count = actual.Count();
            var trueCount = predicted.Zip(actual).Where(p => p.First == p.Second).Count();
            return trueCount / (double)count;
        }
    }
}
