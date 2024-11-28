using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class InvalidValuesColumnProcessor : IPointDataProcessor<double?, double>
    {
        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double?>> data)
        {
            var array = data.To2dArray();
            var rowCount = array.GetLength(0);
            var columnCount = array.GetLength(1);

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
            return array.Cast<IEnumerable<double>>();
        }

        private bool IsInvalidValue(double? value) => value == null || double.IsNaN((double)value);

        private double CalculateReplacementValue(IEnumerable<double> column) => column.Average();
    }
}
