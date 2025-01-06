using MachineLearning.Converters;
using MachineLearning.Interfaces;
using MachineLearning.Scalers;

namespace MachineLearning.Tests.Converters
{
    public class SupervisedLearningConverterPrototype :
        BaseSupervisedLearningConverter<double, IEnumerable<double?>, IEnumerable<double?>, double>
    {
        public SupervisedLearningConverterPrototype
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors) :
            base(primaryPointDataProcessor, pointDataProcessors)
        { }

        public override double ConvertPredicted(double predicted) => predicted + 1;

        protected override IScaler CreateScaler(int index,
            IEnumerable<int> removedColumnsIndices) => new MinMaxScaler();

        protected override IEnumerable<double?> ExtractFeatures(IEnumerable<double?> dataItem) =>
            dataItem.SkipLast(1);

        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures
            (IEnumerable<IEnumerable<double?>> data)
        {
            foreach (var row in data)
            {
                yield return ExtractFeatures(row);
            }
        }

        protected override double ProcessTarget(IEnumerable<double?> item) => (double)item.Last() - 1;

        public IEnumerable<int> NormalizeRemovedIndices
            (IEnumerable<IEnumerable<int>> removedIndicesGroups) =>
            base.NormalizeRemovedIndices(removedIndicesGroups);
    }
}
