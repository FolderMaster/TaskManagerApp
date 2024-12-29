using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class PlannedRealTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, double>
    {
        public PlannedRealTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<Metadata, int?> metadataCategoriesTransformer,
            IDataTransformer<Metadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory,
                metadataCategoriesTransformer, metadataTagsITransformer)
        { }

        public override double ConvertPredicted(double predicted) => predicted;

        protected override List<double?> ExtractPrimaryFeatures(ITaskElement dataItem) =>
            new List<double?>()
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        protected override double ProcessTarget(ITaskElement item) => item.PlannedReal;
    }
}
