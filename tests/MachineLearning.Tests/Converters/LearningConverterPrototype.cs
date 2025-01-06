using MachineLearning.Converters;
using MachineLearning.Interfaces;

namespace MachineLearning.Tests.Converters
{
    public class LearningConverterPrototype :
        BaseLearningConverter<double, IEnumerable<double?>, IEnumerable<double?>, double>
    {
        public IEnumerable<int>? RemovedColumnsIndices
        {
            get => _removedColumnsIndices;
            set => _removedColumnsIndices = value;
        }

        public IEnumerable<IScaler>? Scalers
        {
            get => _scalers;
            set => _scalers = value;
        }

        public LearningConverterPrototype(IPrimaryPointDataProcessor primaryPointDataProcessor) :
            base(primaryPointDataProcessor)
        { }

        protected override IEnumerable<double?> ExtractFeatures(IEnumerable<double?> dataItem) =>
            dataItem;

        public override double ConvertPredicted(double predicted)
        {
            throw new NotImplementedException();
        }
    }
}
