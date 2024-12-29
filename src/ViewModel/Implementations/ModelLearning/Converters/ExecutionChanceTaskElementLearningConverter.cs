using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    public class ExecutionChanceTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, double>
    {
        public ExecutionChanceTaskElementLearningConverter
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
                dataItem.Difficult,
                dataItem.Deadline != null ? dataItem.Deadline.Value.Ticks : null,
                dataItem.PlannedReal,
                dataItem.PlannedTime.TotalSeconds,
                (int)dataItem.Status
            };

        protected override double ProcessTarget(ITaskElement item) => item.Status switch
        {
            TaskStatus.Closed  => 1,
            TaskStatus.Cancelled => 0,
            _ => item.Deadline == null ? 1 : CalculateExecutionChance(item)
        };

        private double CalculateExecutionChance(ITaskElement taskElement)
        {
            if (taskElement.PlannedTime.TotalSeconds <= 0 ||
                (taskElement.Deadline != null && taskElement.Deadline <= DateTime.Now))
            {
                return 0;
            }
            var normalizedTimeDifference = taskElement.SpentTime.TotalSeconds -
                taskElement.PlannedTime.TotalSeconds / taskElement.PlannedTime.TotalSeconds;
            var leftTime = (taskElement.Deadline.Value - DateTime.Now).TotalSeconds;
            return leftTime / (leftTime + normalizedTimeDifference);
        }
    }
}
