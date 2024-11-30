using Accord.Statistics;
using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class OutlierRowProcessor : IPointDataProcessor<double, double>
    {
        private static readonly double _threshold = 2;

        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data)
        {
            var array = data.To2dArray();

            var zScores = array.ZScores();
            var filteredArray = array.Where((_, rowIndex) =>
                !zScores.GetRow(rowIndex).Any(z => Math.Abs(z) >= _threshold)).ToArray();
            return filteredArray;
        }
    }
}
