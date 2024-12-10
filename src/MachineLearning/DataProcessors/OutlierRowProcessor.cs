using Accord.Statistics;
using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика строк для устранения выбросов.
    /// Реализует <see cref="IPointDataProcessor{double}"/>.
    /// </summary>
    public class OutlierRowProcessor : IPointDataProcessor<double>
    {
        /// <summary>
        /// Порог.
        /// </summary>
        private static readonly double _threshold = 2;

        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();

            var zScores = array.ZScores();
            var removedRowsIndices = new List<int>();
            var filteredArray = array.Where((_, i) =>
            {
                var isRowValid = !zScores.GetRow(i).Any(z => Math.Abs(z) >= _threshold);
                if (!isRowValid)
                {
                    removedRowsIndices.Add(i);
                }
                return isRowValid;
            }).ToArray();
            return new DataProcessorResult<IEnumerable<double>>(filteredArray,
                removedRowsIndices: removedRowsIndices);
        }
    }
}
