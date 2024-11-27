using Accord.Math.Optimization.Losses;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class RSquaredScoreMetric : IRegressionModelScoreMetric
    {
        public double CalculateScore(IEnumerable<double> expected, IEnumerable<double> predicted)
        {
            var loss = new RSquaredLoss(predicted.Count(), predicted.ToArray());
            return loss.Loss(expected.ToArray());
        }
    }
}
