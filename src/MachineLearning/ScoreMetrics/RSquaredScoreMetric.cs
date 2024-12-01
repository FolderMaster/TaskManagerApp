using Accord.Math.Optimization.Losses;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class RSquaredScoreMetric : IRegressionScoreMetric
    {
        public double CalculateScore(IEnumerable<double> actual, IEnumerable<double> predicted)
        {
            var loss = new RSquaredLoss(predicted.Count(), predicted.ToArray());
            return loss.Loss(actual.ToArray());
        }
    }
}
