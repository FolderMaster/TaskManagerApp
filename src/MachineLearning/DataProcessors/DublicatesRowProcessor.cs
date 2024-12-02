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
        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data) =>
            data.To2dArray().Distinct().Transpose();
    }
}
