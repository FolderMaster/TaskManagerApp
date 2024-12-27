using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика строк для устранения дубликатов.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPointDataProcessor"/>.
    /// </remarks>
    public class DuplicatesRowProcessor : IPointDataProcessor
    {
        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>> Process
            (IEnumerable<IEnumerable<double>> data)
        {
            var dataArray = data.To2dArray();
            var uniqueRows = new List<double[]>();
            var removedRowsIndices = new List<int>();
            var index = 0;
            foreach (var row in dataArray)
            {
                if (!uniqueRows.Any(r => AreRowsEqual(row, r)))
                {
                    uniqueRows.Add(row);
                }
                else
                {
                    removedRowsIndices.Add(index);
                }
                ++index;
            }
            return new DataProcessorResult<IEnumerable<double>>(uniqueRows,
                removedRowsIndices: removedRowsIndices);
        }

        /// <summary>
        /// Проверяет строки на равенство.
        /// </summary>
        /// <param name="row1">Первая строка.</param>
        /// <param name="row2">Вторая строка.</param>
        /// <returns>Возращает, если строки равны, <c>true</c>, иначе <c>false</c>.</returns>
        private bool AreRowsEqual(double[] row1, double[] row2)
        {
            var count = row1.Length;

            for (int i = 0; i < count; ++i)
            {
                if (row1[i] != row2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
