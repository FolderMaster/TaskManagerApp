using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс конвертора элементраных задач в данные для предсказания срока с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElementSupervisedLearningConverter{double, DateTime?}"/>.
    /// </remarks>
    public class DeadlineTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, DateTime?>
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="DeadlineTaskElementLearningConverter"/>.
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
        public DeadlineTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<TaskMetadata, int?> metadataCategoriesTransformer,
            IDataTransformer<TaskMetadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory,
                metadataCategoriesTransformer, metadataTagsITransformer) { }

        /// <inheritdoc/>
        public override DateTime? ConvertPredicted(double predicted) =>
            predicted > 0 ? new DateTime((long)predicted) : null;

        /// <inheritdoc/>
        protected override List<double?> ExtractPrimaryFeatures(ITaskElement dataItem) =>
            new List<double?>()
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        /// <inheritdoc/>
        protected override double ProcessTarget(ITaskElement item) =>
            item.Deadline != null ? item.Deadline.Value.Ticks : -1;
    }
}
