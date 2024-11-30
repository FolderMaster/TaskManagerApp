using Accord.Math;
using Accord.Statistics;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class LowVariationColumnProcessor : IPointDataProcessor<double, double>
    {
        private static readonly double _threshold = 0.5;

        private static readonly double _lowVariationRatio = 0.8;

        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();
            var columnCount = array.First().Length;
            var lowVariationRowCount = _lowVariationRatio * array.Length;

            var zScores = array.ZScores();
            var removingColumns = new List<int>();
            for (var i = 0; i < columnCount; ++i)
            {
                var column = zScores.GetColumn(i);

                var lowVarianceRowCount = column.Where(v => Math.Abs(v) <= _threshold).Count();
                if (lowVarianceRowCount >= lowVariationRowCount)
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
