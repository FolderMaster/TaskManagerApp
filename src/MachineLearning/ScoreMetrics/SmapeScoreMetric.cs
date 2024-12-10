using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики оценки SMAPE для модели обучения регрессии.
    /// Реализует <see cref="IRegressionScoreMetric"/>.
    /// </summary>
    public class SmapeScoreMetric : IRegressionScoreMetric
    {
        /// <inheritdoc />
        public double CalculateScore(IEnumerable<double> actual, IEnumerable<double> predicted)
        {
            var result = 0d;
            var count = actual.Count();

            for (var n = 0; n < count; ++n)
            {
                var actualValue = actual.ElementAt(n);
                var predictedValue = predicted.ElementAt(n);
                var value = actualValue == predictedValue ? 0 : 2 *
                    Math.Abs(actualValue - predictedValue) /
                    (Math.Abs(predictedValue) + Math.Abs(actualValue));
                result += value;
            }
            return result / count;
        }

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.5 => ScoreMetricCategory.Horrible,
            < 0.5 and >= 0.3 => ScoreMetricCategory.Bad,
            < 0.3 and >= 0.2 => ScoreMetricCategory.Satisfactory,
            < 0.2 and >= 0.1 => ScoreMetricCategory.Good,
            _ => ScoreMetricCategory.Excellent
        };
    }
}
