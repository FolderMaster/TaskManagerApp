using Accord.Math;

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
        /// Обработчики точечных данных.
        /// </summary>
        protected readonly IEnumerable<IPointDataProcessor> _pointDataProcessors;

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
        /// <param name="pointDataProcessors">Обработчики точечных данных.</param>
        protected BaseLearningConverter(IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors)
        {
            _primaryPointDataProcessor = primaryPointDataProcessor;
            _pointDataProcessors = pointDataProcessors;
        }

        /// <inheritdoc />
        public IEnumerable<double> ConvertData(DT data)
        {
            var featuresList = ExtractFeatures(data).ToList();
            foreach (var index in _removedColumnsIndices.OrderDescending())
            {
                featuresList.Remove(index);
            }
            var featuresProcessorResult = _primaryPointDataProcessor.Process(featuresList).Result;
            var featuresScalersResult = featuresProcessorResult.
                Select((r, i) => _scalers.ElementAt(i).Transform(r));
            return featuresScalersResult;
        }

        /// <inheritdoc />
        public abstract DR ConvertPredicted(R predicted);

        /// <summary>
        /// Обрабатывает признаков данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает данные о преобразовании признаков данных.</returns>
        protected DataProcessorResult<IEnumerable<double>> ProcessFeaturesData
            (IEnumerable<D> data)
        {
            var featuresArray = ProcessFeatures(data).To2dArray();
            var removedColumnsIndicesGroups = new List<IEnumerable<int>>();
            var removedRowsIndicesGroups = new List<IEnumerable<int>>();

            var primaryProcessorResult = _primaryPointDataProcessor.Process(featuresArray);
            removedColumnsIndicesGroups.Add(primaryProcessorResult.RemovedColumnsIndices);
            removedRowsIndicesGroups.Add(primaryProcessorResult.RemovedRowsIndices);
            var newFeaturesArray = primaryProcessorResult.Result;

            foreach (var pointDataProcessor in _pointDataProcessors)
            {
                var processorResult = pointDataProcessor.Process(newFeaturesArray);
                removedColumnsIndicesGroups.Add(processorResult.RemovedColumnsIndices);
                removedRowsIndicesGroups.Add(processorResult.RemovedRowsIndices);
                newFeaturesArray = processorResult.Result;
            }

            var removedColumnsIndices = NormalizeRemovedIndices(removedColumnsIndicesGroups);
            var removedRowIndices = NormalizeRemovedIndices(removedRowsIndicesGroups);
            return new DataProcessorResult<IEnumerable<double>>
                (newFeaturesArray, removedColumnsIndices, removedRowIndices);
        }

        /// <summary>
        /// Создаёт коллекцию масштабирования данных.
        /// </summary>
        /// <param name="featuresArray">Массив признаков.</param>
        protected void CreateScalers(double[][] featuresArray)
        {
            var columnCount = featuresArray.First().Length;
            var scalers = new List<IScaler>();
            for (var i = 0; i < columnCount; ++i)
            {
                var scaler = CreateScaler(i, _removedColumnsIndices);
                var column = featuresArray.GetColumn(i);
                var newColumn = scaler.FitTransform(column).ToArray();
                featuresArray.SetColumn(i, newColumn);
                scalers.Add(scaler);
            }
            _scalers = scalers;
        }

        /// <summary>
        /// Обрабатывает признаков данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает обработанные признаков данных.</returns>
        protected abstract IEnumerable<IEnumerable<double?>> ProcessFeatures(IEnumerable<D> data);

        /// <summary>
        /// Извлекает признаки у элемента данных.
        /// </summary>
        /// <param name="dataItem">Элемент данных.</param>
        /// <returns>Возвращет, извлеченное у элемента данных, целевое значение.</returns>
        protected abstract IEnumerable<double?> ExtractFeatures(DT dataItem);

        /// <summary>
        /// Создаёт масштабирование данных.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="removedColumnsIndices">Индексы удалённых столбцов.</param>
        /// <returns>Возвращает масштабирование данных.</returns>
        protected abstract IScaler CreateScaler(int index, IEnumerable<int> removedColumnsIndices);

        /// <summary>
        /// Нормализует индексы удалённых индексов.
        /// </summary>
        /// <param name="removedIndicesGroups">Группы индексов удалённых индексов.</param>
        /// <returns>Возвращает нормализованные удалённые индексы.</returns>
        private IEnumerable<int> NormalizeRemovedIndices
            (IEnumerable<IEnumerable<int>> removedIndicesGroups)
        {
            var result = new List<int>();
            foreach (var removedIndices in removedIndicesGroups)
            {
                var newNormalizedIndices = new List<int>();
                foreach (var index in removedIndices.Order())
                {
                    var normalizedIndex = index;
                    foreach (var removedIndex in result)
                    {
                        if (normalizedIndex > removedIndex)
                        {
                            break;
                        }
                        ++normalizedIndex;
                    }
                    newNormalizedIndices.Add(normalizedIndex);
                }
                result.AddRange(newNormalizedIndices);
                result = result.Order().ToList();
            }
            return result;
        }
    }
}
