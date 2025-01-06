using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    /// <summary>
    /// Класс обработчика столбцов для устранения некорректных значений.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IPrimaryPointDataProcessor"/>.
    /// </remarks>
    public class InvalidValuesColumnProcessor : IPrimaryPointDataProcessor
    {
        /// <summary>
        /// Значение для замены некорректных значений.
        /// </summary>
        protected double _replacementInvalidValue = -1;

        /// <inheritdoc />
        public DataProcessorResult<double> Process(IEnumerable<double?> data)
        {
            var result = data.Select(v => IsInvalidValue(v) ? _replacementInvalidValue : v.Value);
            return new DataProcessorResult<double>(result);
        }

        /// <inheritdoc />
        public DataProcessorResult<IEnumerable<double>> Process
            (IEnumerable<IEnumerable<double?>> data)
        {
            var array = data.To2dArray();
            var rowCount = array.Length;
            var columnCount = array.First().Length;
            var removingColumns = new List<int>();

            for (var n = 0; n < columnCount; ++n)
            {
                var column = array.GetColumn(n);
                if (column.All(IsInvalidValue))
                {
                    removingColumns.Add(n);
                }
                else if (column.Any(IsInvalidValue))
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

            foreach (var column in removingColumns.OrderDescending())
            {
                array = array.RemoveColumn(column);
            }
            return new DataProcessorResult<IEnumerable<double>>
                (array.Select(a => a.Select(v => (double)v)), removingColumns);
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
