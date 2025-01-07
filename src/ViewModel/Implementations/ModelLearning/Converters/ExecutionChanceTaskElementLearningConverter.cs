using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс конвертора элементраных задач в данные для предсказания шанс выполнения
    /// с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElementSupervisedLearningConverter{double, double}"/>.
    /// </remarks>
    public class ExecutionChanceTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, double>
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="ExecutionChanceTaskElementLearningConverter"/>.
        /// </summary>
        /// <param name="primaryPointDataProcessor">Первичный обработчик точечных данных.</param>
        /// <param name="pointDataProcessors">Обработчики точечных данных.</param>
        /// <param name="scalerFactory">Фабрика, создающая масштабирования данных.</param>
        /// <param name="metadataCategoriesTransformer">
        /// Преобразование категории метаданных в данные для предсказания.
        /// </param>
        /// <param name="metadataTagsITransformer">
        /// Преобразование теги метаданных в данные для предсказания.
        /// </param>
        public ExecutionChanceTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<TaskMetadata, int?> metadataCategoriesTransformer,
            IDataTransformer<TaskMetadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory,
                metadataCategoriesTransformer, metadataTagsITransformer)
        { }

        /// <inheritdoc/>
        public override double ConvertPredicted(double predicted) => predicted;

        /// <inheritdoc/>
        protected override List<double?> ExtractPrimaryFeatures(ITaskElement dataItem) =>
            new List<double?>()
            {
                dataItem.Priority,
                dataItem.Difficult,
                dataItem.Deadline != null ? dataItem.Deadline.Value.Ticks : null,
                dataItem.PlannedReal,
                dataItem.ExecutedReal,
                dataItem.PlannedTime.TotalSeconds,
                dataItem.SpentTime.TotalSeconds,
                (int)dataItem.Status
            };

        /// <inheritdoc/>
        protected override double ProcessTarget(ITaskElement item) => item.Status switch
        {
            TaskStatus.Closed  => 1,
            TaskStatus.Cancelled => 0,
            _ => item.Deadline == null ? 1 : CalculateExecutionChance(item)
        };

        /// <summary>
        /// Рассчитывает шанс выполнения.
        /// </summary>
        /// <param name="taskElement">Элементарная задача.</param>
        /// <returns>Возвращает шанс выполнения.</returns>
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
