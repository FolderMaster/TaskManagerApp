using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class DeadlineTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, DateTime?>
    {
        public DeadlineTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<Metadata, int?> metadataCategoriesTransformer,
            IDataTransformer<Metadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory,
                metadataCategoriesTransformer, metadataTagsITransformer) { }

        public override DateTime? ConvertPredicted(double predicted) =>
            predicted > 0 ? new DateTime((long)predicted) : null;

        protected override List<double?> ExtractPrimaryFeatures(ITaskElement dataItem) =>
            new List<double?>()
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        protected override double ProcessTarget(ITaskElement item) =>
            item.Deadline != null ? item.Deadline.Value.Ticks : -1;
    }
}
