using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    public class AdjustedRandIndexScoreMetric : IClassificationScoreMetric
    {
        public double CalculateScore(IEnumerable<int> actual, IEnumerable<int> predicted)
        {
            var count = actual.Count();

            var actualUniqueValues = actual.Distinct().ToArray();
            var actualUniqueValuesCount = actualUniqueValues.Count();
            var predictedUniqueValues = predicted.Distinct().ToArray();
            var predictedUniqueValuesCount = predictedUniqueValues.Count();
            var matrix = new int[actualUniqueValuesCount, predictedUniqueValuesCount];
            for (var i = 0; i < count; ++i)
            {
                var actualIndex = actualUniqueValues.IndexOf(actual.ElementAt(i));
                var predictedValue = predictedUniqueValues.IndexOf(predicted.ElementAt(i));

                ++matrix[actualIndex, predictedValue];
            }

            var index = 0;
            for (var i = 0; i < actualUniqueValuesCount; ++i)
            {
                for (var n = 0; n < predictedUniqueValuesCount; ++n)
                {
                    index += CalculateCombinationsCount(matrix[i, n], 2);
                }
            }
            var columnCombinationsCount = 0;
            for (var i = 0; i < actualUniqueValuesCount; ++i)
            {
                columnCombinationsCount +=
                    CalculateCombinationsCount(matrix.GetColumn(i).Sum(), 2);
            }
            var rowCombinationsCount = 0;
            for (var n = 0; n < predictedUniqueValuesCount; ++n)
            {
                rowCombinationsCount +=
                    CalculateCombinationsCount(matrix.GetRow(n).Sum(), 2);
            }
            var totalCombinationsCount = CalculateCombinationsCount(count, 2);
            var expectedIndex = rowCombinationsCount *
                columnCombinationsCount / (double)totalCombinationsCount;
            var maxIndex = 0.5 * (rowCombinationsCount + columnCombinationsCount);

            var dividend = index - expectedIndex;
            var divisor = maxIndex - expectedIndex;
            if (dividend == divisor)
            {
                return 1;
            }
            return dividend / divisor;
        }

        private int CalculateCombinationsCount(int n, int k)
        {
            if (n < k)
            {
                return 0;
            }
            if (k == 0 || n == k)
            {
                return 1;
            }
            return CalculateFactorial(n) / (CalculateFactorial(k) * CalculateFactorial(n - k));
        }

        private int CalculateFactorial(int n)
        {
            var result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
