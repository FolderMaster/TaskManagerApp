using Accord.Math;
using Accord.Statistics;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика столбцов для устранения низкой вариативности.
    /// Реализует <see cref="IPointDataProcessor{double}"/>.
    /// </summary>
    public class LowVariationColumnProcessor : IPointDataProcessor<double>
    {
        /// <summary>
        /// Порог.
        /// </summary>
        private static readonly double _threshold = 0.5;

        /// <summary>
        /// Соотношение количества строк с низкой вариативностью.
        /// </summary>
        private static readonly double _lowVariationRowCountRatio = 0.8;

        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>>
            Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();
            var columnCount = array.First().Length;
            var lowVariationRowCount = _lowVariationRowCountRatio * array.Length;

            var zScores = array.ZScores();
            var removingColumns = new List<int>();
            for (var i = 0; i < columnCount; ++i)
            {
                var column = zScores.GetColumn(i);

                var lowVarianceRowCount = column.Where(v => Math.Abs(v) <= _threshold).Count();
                if (lowVarianceRowCount >= lowVariationRowCount)
                {
                    removingColumns.Add(i);
                }
            }

            removingColumns.OrderDescending();
            foreach (var column in removingColumns)
            {
                array = array.RemoveColumn(column);
            }
            return new DataProcessorResult<IEnumerable<double>>(array, removingColumns);
        }
    }
}
