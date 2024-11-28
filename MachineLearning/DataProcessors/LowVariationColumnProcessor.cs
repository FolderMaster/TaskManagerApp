using Accord.Math;
using Accord.Statistics;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class LowVariationColumnProcessor : IPointDataProcessor<double, double>
    {
        private static readonly double _threshold = 0.1;

        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();
            var columnCount = array.GetLength(1);

            var removingColumns = new List<int>();
            for (var i = 0; i < columnCount; ++i)
            {
                var column = array.GetColumn(i);

                var mean = column.Average();
                var standardDeviation = Measures.StandardDeviation(column);
                var coefficientVariation = standardDeviation / mean;
                if (coefficientVariation < _threshold)
                {
                    removingColumns.Add(i);
                }
            }

            removingColumns.OrderDescending();
            foreach (var column in removingColumns)
            {
                array = array.RemoveColumn(column);
            }
            return array;
        }
    }
}
