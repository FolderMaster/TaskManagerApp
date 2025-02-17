using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.ScoreMetrics
{
    /// <summary>
    /// Класс метрики оценки скорректированного индекса Рэнда для модели обучения классификации
    /// на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPredictedClusteringScoreMetric"/>.
    /// </remarks>
    public class AdjustedRandIndexScoreMetric : IPredictedClusteringScoreMetric
    {
        /// <inheritdoc />
        public double CalculateScore(IEnumerable<int> predicted1, IEnumerable<int> predicted2)
        {
            var count = predicted1.Count();

            var predicted1UniqueValues = predicted1.Distinct().ToArray();
            var predicted1UniqueValuesCount = predicted1UniqueValues.Count();
            var predicted2UniqueValues = predicted2.Distinct().ToArray();
            var predicted2UniqueValuesCount = predicted2UniqueValues.Count();
            var matrix = new int[predicted1UniqueValuesCount, predicted2UniqueValuesCount];
            for (var i = 0; i < count; ++i)
            {
                var actualIndex = predicted1UniqueValues.IndexOf(predicted1.ElementAt(i));
                var predictedValue = predicted2UniqueValues.IndexOf(predicted2.ElementAt(i));

                ++matrix[actualIndex, predictedValue];
            }

            var index = 0;
            for (var i = 0; i < predicted1UniqueValuesCount; ++i)
            {
                for (var n = 0; n < predicted2UniqueValuesCount; ++n)
                {
                    index += CalculateCombinationsCount(matrix[i, n], 2);
                }
            }
            var columnCombinationsCount = 0;
            for (var i = 0; i < predicted1UniqueValuesCount; ++i)
            {
                columnCombinationsCount +=
                    CalculateCombinationsCount(matrix.GetRow(i).Sum(), 2);
            }
            var rowCombinationsCount = 0;
            for (var n = 0; n < predicted2UniqueValuesCount; ++n)
            {
                rowCombinationsCount +=
                    CalculateCombinationsCount(matrix.GetColumn(n).Sum(), 2);
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

        /// <inheritdoc />
        public ScoreMetricCategory GetScoreCategory(double score) => score switch
        {
            >= 0.9 => ScoreMetricCategory.Excellent,
            < 0.9 and >= 0.8 => ScoreMetricCategory.Good,
            < 0.8 and >= 0.6 => ScoreMetricCategory.Satisfactory,
            < 0.6 and >= 0.3 => ScoreMetricCategory.Bad,
            _ => ScoreMetricCategory.Horrible
        };

        /// <summary>
        /// Вычисляет количество комбинаций, в которых можно выбрать <paramref name="k"/>
        /// элементов из <paramref name="n"/> элементов без учета порядка.
        /// </summary>
        /// <param name="n">Общее количество элементов.</param>
        /// <param name="k">Количество элементов в комбинации.</param>
        /// <returns>Возвращает количество комбинаций или <c>0</c>, если <paramref name="n"/>
        /// меньше <paramref name="k"/>, <c>1</c>, если <paramref name="n"/> равен
        /// <paramref name="k"/> или <paramref name="k"/> равен <c>0</c>.</returns>
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

        /// <summary>
        /// Вычисляет факториал числа <paramref name="n"/>.
        /// </summary>
        /// <param name="n">Число, для которого вычисляется факториал.</param>
        /// <returns>Возвращает факториал числа <paramref name="n"/>.</returns>
        private int CalculateFactorial(int n)
        {
            var result = 1;
            for (var i = 2; i <= n; ++i)
            {
                result *= i;
            }
            return result;
        }
    }
}
