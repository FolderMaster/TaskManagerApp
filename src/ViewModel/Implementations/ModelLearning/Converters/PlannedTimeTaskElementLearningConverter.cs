using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class PlannedTimeTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, TimeSpan>
    {
        public PlannedTimeTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory)
        { }

        public override TimeSpan ConvertPredicted(double predicted) =>
            new TimeSpan((long)predicted);

        protected override IEnumerable<double?> ExtractFeatures(ITaskElement dataItem) =>
            new double?[]
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures
            (IEnumerable<ITaskElement> data)
        {
            foreach (var dataItem in data)
            {
                yield return ExtractFeatures(dataItem);
            }
        }

        protected override double ProcessTarget(ITaskElement item) => item.PlannedTime.Ticks;
    }
}
