using MachineLearning.Interfaces;

using Model.Interfaces;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class ProgressTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, double>
    {
        public ProgressTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory)
        { }

        public override double ConvertPredicted(double predicted) => predicted;

        protected override IEnumerable<double?> ExtractFeatures(ITaskElement dataItem) =>
            new double?[]
            {
                dataItem.Priority,
                dataItem.Difficult,
                dataItem.Deadline != null ? dataItem.Deadline.Value.Ticks : null,
                dataItem.PlannedReal,
                dataItem.PlannedTime.Ticks,
                (int)dataItem.Status
            };

        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures
            (IEnumerable<ITaskElement> data)
        {
            foreach (var dataItem in data)
            {
                yield return ExtractFeatures(dataItem);
            }
        }

        protected override double ProcessTarget(ITaskElement item) => item.Progress;
    }
}
