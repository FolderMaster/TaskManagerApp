using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class AccuracyScoreMetric : IClassificationScoreMetric
    {
        public double CalculateScore(IEnumerable<int> expected, IEnumerable<int> predicted)
        {
            var count = expected.Count();
            var trueCount = predicted.Zip(expected).Where(p => p.First == p.Second).Count();
            return trueCount / (double)count;
        }
    }
}
