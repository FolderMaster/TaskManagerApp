using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class F1ScoreMetric : IClassificationScoreMetric
    {
        public double CalculateScore(IEnumerable<int> expected, IEnumerable<int> predicted)
        {
            var count = expected.Count();

            var uniqueValues = expected.Union(predicted).Distinct().ToArray();
            var uniqueValuesCount = uniqueValues.Count();
            var matrix = new double[uniqueValuesCount, uniqueValuesCount];
            for (var i = 0; i < count; ++i)
            {
                var expectedIndex = uniqueValues.IndexOf(expected.ElementAt(i));
                var predictedValue = uniqueValues.IndexOf(predicted.ElementAt(i));

                ++matrix[expectedIndex, predictedValue];
            }
            var values = new double[uniqueValuesCount];
            for (var i = 0; i < uniqueValuesCount; ++i)
            {
                var value = matrix[i, i];
                var precisionDivider = matrix.GetColumn(i).Sum();
                var recallDivider = matrix.GetRow(i).Sum();

                var precision = precisionDivider > 0 ? value / (double)precisionDivider : 0;
                var recall = recallDivider > 0 ? value / (double)recallDivider : 0;
                values[i] = precision == 0 && recall == 0 ? 0 :
                    2 * precision * recall / (double)(precision + recall);
            }
            return values.Average();
        }
    }
}
