using Accord.Statistics.Analysis;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class F1ScoreMetric : IClassificationScoreMetric
    {
        public double CalculateScore(IEnumerable<int> expected, IEnumerable<int> predicted)
        {
            var matrix = new ConfusionMatrix(predicted.ToArray(), expected.ToArray());
            return matrix.FScore;
        }
    }
}
