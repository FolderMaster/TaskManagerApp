using Accord.Math;
using Accord.Statistics;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class CorrelationColumnProcessor : IPointDataProcessor<double, double>
    {
        private static readonly double _threshold = 0.9;

        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();
            var columnCount = array.GetLength(1);

            var correlationArray = array.Correlation();
            var correlationDictionary = new List<(int, int)>();
            var removingColumns = new List<int>();
            for (var n = 0; n < columnCount; ++n)
            {
                for (var i = n + 1; i < columnCount; ++i) 
                {
                    var value = correlationArray[i][n];
                    if (value < _threshold)
                    {
                        correlationDictionary.Add((i, n));
                        correlationDictionary.Add((n, i));
                    }
                }
            }
            while (correlationDictionary.Count > 0)
            {
                var removingColumn = correlationDictionary.GroupBy(c => c.Item1).
                    OrderByDescending(c => c.Count()).First().Key;
                removingColumns.Add(removingColumn);
                correlationDictionary.RemoveAll(c => c.Item1 == removingColumn ||
                    c.Item2 == removingColumn);
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
