using Accord.Math.Optimization.Losses;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики R² оценки для модели обучения регрессии.
    /// Реализует <see cref="IRegressionScoreMetric"/>.
    /// </summary>
    public class RSquaredScoreMetric : IRegressionScoreMetric
    {
        /// <inheritdoc />
        public double CalculateScore(IEnumerable<double> actual, IEnumerable<double> predicted)
        {
            var loss = new RSquaredLoss(predicted.Count(), predicted.ToArray());
            return loss.Loss(actual.ToArray());
        }
    }
}
