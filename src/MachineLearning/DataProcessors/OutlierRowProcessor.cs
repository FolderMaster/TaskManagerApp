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
        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();

            var zScores = array.ZScores();
            var filteredArray = array.Where((_, rowIndex) =>
                !zScores.GetRow(rowIndex).Any(z => Math.Abs(z) >= _threshold)).ToArray();
            return filteredArray;
        }
    }
}
