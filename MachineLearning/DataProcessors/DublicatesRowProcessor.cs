using Accord.Math;

using MachineLearning.Interfaces;

namespace MachineLearning.DataProcessors
{
    public class DuplicatesRowProcessor : IPointDataProcessor<double, double>
    {
        public IEnumerable<IEnumerable<double>> Process(IEnumerable<IEnumerable<double>> data) =>
            data.To2dArray().Distinct();
    }
}
