using DynamicData;
using MachineLearning.Converters;
using MachineLearning.Interfaces;

using Model.Interfaces;

using ViewModel.Interfaces.DataManagers.Generals;
using ViewModel.Interfaces.ModelLearning;
using ViewModel.Technicals;

namespace ViewModel.Implementations.ModelLearning.Converters
{
    /// <summary>
    /// Абстрактный класс базового конвертора элементраных задач в данные
    /// для предсказания с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseSupervisedLearningConverter{R, ITaskElement, ITaskElement, DR}"/>.
    /// </remarks>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public abstract class BaseTaskElementSupervisedLearningConverter<R, DR> :
        BaseSupervisedLearningConverter<R, ITaskElement, ITaskElement, DR>
    {
        /// <summary>
        /// Фабрика, создающая масштабирования данных.
        /// </summary>
        protected readonly IFactory<IScaler> _scalerFactory;

        /// <summary>
        /// Преобразование категории метаданных в данные для предсказания.
        /// </summary>
        protected readonly IDataTransformer<TaskMetadata, int?> _metadataCategoriesTransformer;

        /// <summary>
        /// Преобразование теги метаданных в данные для предсказания.
        /// </summary>
        protected readonly IDataTransformer<TaskMetadata, IEnumerable<int>> _metadataTagsTransformer;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseTaskElementSupervisedLearningConverter{R, DR}"/>.
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
        protected BaseTaskElementSupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors,
            IFactory<IScaler> scalerFactory,
            IDataTransformer<TaskMetadata, int?> metadataCategoriesTransformer,
            IDataTransformer<TaskMetadata, IEnumerable<int>> metadataTagsITransformer) :
            base(primaryPointDataProcessor, pointDataProcessors)
        {
            _scalerFactory = scalerFactory;
            _metadataCategoriesTransformer = metadataCategoriesTransformer;
            _metadataTagsTransformer = metadataTagsITransformer;
        }

        /// <inheritdoc/>
        protected override IEnumerable<IEnumerable<double?>> ProcessFeatures
            (IEnumerable<ITaskElement> data)
        {
            var metadataSet = data.Select(t => t.Metadata as TaskMetadata);
            var categories = _metadataCategoriesTransformer.FitTransform(metadataSet);
            var tags = _metadataTagsTransformer.FitTransform(metadataSet);
            var index = 0;
            foreach (var dataItem in data)
            {
                var result = ExtractPrimaryFeatures(dataItem);
                result.Add(categories.ElementAt(index));
                result.AddRange(tags.ElementAt(index).Cast<double?>());
                yield return result;
                ++index;
            }
        }

        /// <inheritdoc/>
        protected override IEnumerable<double?> ExtractFeatures(ITaskElement dataItem)
        {
            var metadata = dataItem.Metadata as TaskMetadata;
            var result = ExtractPrimaryFeatures(dataItem);
            result.Add(_metadataCategoriesTransformer.Transform(metadata));
            result.AddRange(_metadataTagsTransformer.Transform(metadata).Cast<double?>());
            return result;
        }

        /// <inheritdoc/>
        protected override IScaler CreateScaler
            (int index, IEnumerable<int> removedColumnsIndices) => _scalerFactory.Create();

        /// <summary>
        /// Извлекает первичные признаки у элемента данных.
        /// </summary>
        /// <param name="dataItem">Элемент данных.</param>
        /// <returns>Возвращает список значений.</returns>
        protected abstract List<double?> ExtractPrimaryFeatures(ITaskElement dataItem);
    }
}
