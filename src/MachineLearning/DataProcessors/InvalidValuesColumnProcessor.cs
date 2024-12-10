using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика столбцов для устранения некорректных значений.
    /// Реализует <see cref="IPointDataProcessor{double?}"/>.
    /// </summary>
    public class InvalidValuesColumnProcessor : IPointDataProcessor<double?>
    {
        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>> Process
            (IEnumerable<IEnumerable<double?>> data)
        {
            var array = data.To2dArray();
            var rowCount = array.Length;
            var columnCount = array.First().Length;

            for (var n = 0; n < columnCount; ++n)
            {
                var column = array.GetColumn(n);
                if (column.Any(IsInvalidValue))
                {
                    var replacementValue = CalculateReplacementValue
                        (column.Where(v => !IsInvalidValue(v)).Cast<double>());
                    for (var i = 0; i < rowCount; ++i)
                    {
                        if (IsInvalidValue(column[i]))
                        {
                            array[i][n] = replacementValue;
                        }
                    }
                }
            }
            return new DataProcessorResult<IEnumerable<double>>
                (array.Select(a => a.Select(v => (double)v)));
        }

        /// <summary>
        /// Проверяет, является ли значение не корректным.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>
        /// Возвращает <c>true</c>, если значение не корректно, иначе <c>false</c>.
        /// </returns>
        protected virtual bool IsInvalidValue(double? value) =>
            value == null || double.IsNaN((double)value);

        /// <summary>
        /// Рассчитывает значение для замещения на основе столбца.
        /// </summary>
        /// <param name="column">Столбец.</param>
        /// <returns>Возвращает значение для замещения.</returns>
        protected virtual double CalculateReplacementValue(IEnumerable<double> column) =>
            column.Average();
    }
}
