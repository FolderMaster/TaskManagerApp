using MachineLearning.Interfaces;
using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Converters
{
    /// <summary>
    /// Абстрактный класс базового конвертора данных в данные для предсказания и наоборот.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ILearningConverter{IEnumerable{double}, R, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public abstract class BaseLearningConverter<R, D, DT, DR> :
        ILearningConverter<IEnumerable<double>, R, DT, DR>
    {
        /// <summary>
        /// Первичный обработчик точечных данных.
        /// </summary>
        protected readonly IPrimaryPointDataProcessor _primaryPointDataProcessor;

        /// <summary>
        /// Индексы удалённых столбцов.
        /// </summary>
        protected IEnumerable<int>? _removedColumnsIndices;

        /// <summary>
        /// Коллекция масштабирования данных.
        /// </summary>
        protected IEnumerable<IScaler>? _scalers;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseLearningConverter{R, D, DT, DR}"/>.
        /// </summary>
        /// <param name="primaryPointDataProcessor">Первичный обработчик точечных данных.</param>
        protected BaseLearningConverter(IPrimaryPointDataProcessor primaryPointDataProcessor)
        {
            _primaryPointDataProcessor = primaryPointDataProcessor;
        }

        /// <inheritdoc />
        public abstract DR ConvertPredicted(R predicted);

        /// <inheritdoc />
        public IEnumerable<double> ConvertData(DT data)
        {
            var featuresList = ExtractFeatures(data).ToList();
            foreach (var index in _removedColumnsIndices.OrderDescending())
            {
                featuresList.RemoveAt(index);
            }
            var featuresProcessorResult = _primaryPointDataProcessor.Process(featuresList).Result;
            var featuresScalersResult = featuresProcessorResult.
                Select((r, i) => _scalers.ElementAt(i).Transform(r));
            return featuresScalersResult;
        }

        /// <summary>
        /// Извлекает признаки у элемента данных.
        /// </summary>
        /// <param name="dataItem">Элемент данных.</param>
        /// <returns>Возвращет, извлеченное у элемента данных, целевое значение.</returns>
        protected abstract IEnumerable<double?> ExtractFeatures(DT dataItem);
    }
}
