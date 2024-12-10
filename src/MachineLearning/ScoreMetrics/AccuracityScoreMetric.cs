using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики оценки точности для модели обучения классификации.
    /// Реализует <see cref="IClassificationScoreMetric"/>.
    /// </summary>
    public class AccuracyScoreMetric : IClassificationScoreMetric
    {
        /// <inheritdoc />
        public double CalculateScore(IEnumerable<int> actual, IEnumerable<int> predicted)
        {
            var count = actual.Count();
            var trueCount = predicted.Zip(actual).Where(p => p.First == p.Second).Count();
            return trueCount / (double)count;
        }

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.95 => ScoreMetricCategory.Excellent,
            < 0.95 and >= 0.85 => ScoreMetricCategory.Good,
            < 0.85 and >= 0.70 => ScoreMetricCategory.Satisfactory,
            < 0.70 and >= 0.50 => ScoreMetricCategory.Bad,
            _ => ScoreMetricCategory.Horrible
        };
    }
}
