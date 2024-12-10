using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики F1 оценки для модели обучения классификации.
    /// Реализует <see cref="IClassificationScoreMetric"/>.
    /// </summary>
    public class F1ScoreMetric : IClassificationScoreMetric
    {
        /// <inheritdoc />
        public double CalculateScore(IEnumerable<int> actual, IEnumerable<int> predicted)
        {
            var count = actual.Count();

            var uniqueValues = actual.Union(predicted).Distinct().ToArray();
            var uniqueValuesCount = uniqueValues.Count();
            var matrix = new double[uniqueValuesCount, uniqueValuesCount];
            for (var i = 0; i < count; ++i)
            {
                var actualIndex = uniqueValues.IndexOf(actual.ElementAt(i));
                var predictedValue = uniqueValues.IndexOf(predicted.ElementAt(i));

                ++matrix[actualIndex, predictedValue];
            }

            var values = new double[uniqueValuesCount];
            for (var i = 0; i < uniqueValuesCount; ++i)
            {
                var value = matrix[i, i];
                var precisionDivider = matrix.GetColumn(i).Sum();
                var recallDivider = matrix.GetRow(i).Sum();

                var precision = precisionDivider > 0 ? value / precisionDivider : 0;
                var recall = recallDivider > 0 ? value / recallDivider : 0;
                values[i] = precision == 0 && recall == 0 ? 0 :
                    2 * precision * recall / (precision + recall);
            }
            return values.Average();
        }

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.9 => ScoreMetricCategory.Excellent,
            < 0.9 and >= 0.8 => ScoreMetricCategory.Good,
            < 0.8 and >= 0.6 => ScoreMetricCategory.Satisfactory,
            < 0.6 and >= 0.4 => ScoreMetricCategory.Bad,
            _ => ScoreMetricCategory.Horrible
        };
    }
}
