using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс конвертора элементраных задач в данные для предсказания запланированного времени
    /// с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElementSupervisedLearningConverter{double, TimeSpan}"/>.
    /// </remarks>
    public class PlannedTimeTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, TimeSpan>
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="PlannedTimeTaskElementLearningConverter"/>.
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
        public PlannedTimeTaskElementLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<Metadata, int?> metadataCategoriesTransformer,
            IDataTransformer<Metadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors, scalerFactory,
                metadataCategoriesTransformer, metadataTagsITransformer)
        { }

        /// <inheritdoc/>
        public override TimeSpan ConvertPredicted(double predicted) =>
            new TimeSpan((long)predicted);

        /// <inheritdoc/>
        protected override List<double?> ExtractPrimaryFeatures(ITaskElement dataItem) =>
            new List<double?>()
            {
                dataItem.Priority,
                dataItem.Difficult
            };

        /// <inheritdoc/>
        protected override double ProcessTarget(ITaskElement item) => item.PlannedTime.Ticks;
    }
}
