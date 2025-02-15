using Accord.Math;

using MachineLearning.Aggregators;
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

        /// <summary>
        /// Возвращает и задаёт агрегатор.
        /// </summary>
        public IAggregator Aggregator { get; set; } = new MeanAggregator();

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
                var invalidValuesCount = column.Count(IsInvalidValue);
                if (invalidValuesCount == rowCount)
                {
                    removingColumns.Add(n);
                }
                else if (invalidValuesCount != 0)
                {
                    var validValues = column.Where(v => !IsInvalidValue(v)).Cast<double>();
                    var replacementValue = Aggregator.AggregateToValue(validValues);
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
    }
}
