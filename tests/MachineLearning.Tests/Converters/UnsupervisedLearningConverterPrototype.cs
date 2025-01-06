using MachineLearning.Converters;
using MachineLearning.Interfaces;
using MachineLearning.Scalers;

namespace MachineLearning.Tests.Converters
{
    public class UnsupervisedLearningConverterPrototype :
        BaseUnsupervisedLearningConverter<double, IEnumerable<double?>, IEnumerable<double?>, double>
    {
        public UnsupervisedLearningConverterPrototype
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

        public IEnumerable<int> NormalizeRemovedIndices
            (IEnumerable<IEnumerable<int>> removedIndicesGroups) =>
            base.NormalizeRemovedIndices(removedIndicesGroups);
    }
}
