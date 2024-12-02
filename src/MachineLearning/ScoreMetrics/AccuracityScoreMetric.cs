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
    }
}
