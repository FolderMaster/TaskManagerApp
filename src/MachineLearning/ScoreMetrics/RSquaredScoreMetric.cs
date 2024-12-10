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

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.9 => ScoreMetricCategory.Excellent,
            < 0.9 and >= 0.8 => ScoreMetricCategory.Good,
            < 0.8 and >= 0.6 => ScoreMetricCategory.Satisfactory,
            < 0.6 and >= 0.3 => ScoreMetricCategory.Bad,
            _ => ScoreMetricCategory.Horrible
        };
    }
}
