using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class DeadlineTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, DateTime?>
    {
        public DeadlineTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory) { }

        public override DateTime? ConvertPredicted(double predicted) =>
            predicted > 0 ? new DateTime((long)predicted) : null;

        protected override IEnumerable<double?> ExtractFeatures(ITaskElement dataItem) =>
            new double?[]
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures(IEnumerable<ITaskElement> data)
        {
            foreach (var dataItem in data)
            {
                yield return ExtractFeatures(dataItem);
            }
        }

        protected override double ProcessTarget(ITaskElement item) =>
            item.Deadline != null ? item.Deadline.Value.Ticks : -1;
    }
}
