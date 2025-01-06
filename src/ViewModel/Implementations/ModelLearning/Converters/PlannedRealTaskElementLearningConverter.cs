using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Класс конвертора элементраных задач в данные для предсказания
    /// запланированного реального показателя с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseTaskElementSupervisedLearningConverter{double, double}"/>.
    /// </remarks>
    public class PlannedRealTaskElementLearningConverter :
        BaseTaskElementSupervisedLearningConverter<double, double>
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="PlannedRealTaskElementLearningConverter"/>.
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
        public PlannedRealTaskElementLearningConverter
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
                dataItem.Difficult
            };

        /// <inheritdoc/>
        protected override double ProcessTarget(ITaskElement item) => item.PlannedReal;
    }
}
