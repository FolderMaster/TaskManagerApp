using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика строк для устранения дубликатов.
    /// Реализует <see cref="IPointDataProcessor{double}"/>.
    /// </summary>
    public class DuplicatesRowProcessor : IPointDataProcessor<double>
    {
        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>> Process
            (IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();

            var result = array.Distinct().Transpose();
            var removedRowsIndices = Enumerable.Range(0, array.Length).
                Except(result.Select((_, index) => index));
            return new DataProcessorResult<IEnumerable<double>>(result,
                removedRowsIndices: removedRowsIndices);
        }
    }
}
