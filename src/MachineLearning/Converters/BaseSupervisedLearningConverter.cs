using Accord.Math;

using MachineLearning.Interfaces;
using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Converters
{
    /// <summary>
    /// Абстрактный класс базового конвертора данных в данные
    /// для предсказания с учителем и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseLearningConverter{R, D, DT, DR}"/>.
    /// Реализует <see cref="ISupervisedLearningConverter{IEnumerable{double}, R, D, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public abstract class BaseSupervisedLearningConverter<R, D, DT, DR> :
        BaseLearningConverter<R, D, DT, DR>,
        ISupervisedLearningConverter<IEnumerable<double>, R, D, DT, DR>
    {
        /// <summary>
        /// Обработчики точечных данных.
        /// </summary>
        protected readonly IEnumerable<IPointDataProcessor> _pointDataProcessors;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseSupervisedLearningConverter{R, D, DT, DR}"/>.
        /// </summary>
        /// <param name="primaryPointDataProcessor">Первичный обработчик точечных данных.</param>
        /// <param name="pointDataProcessors">Обработчики точечных данных.</param>
        protected BaseSupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors) : base(primaryPointDataProcessor)
        {
            _pointDataProcessors = pointDataProcessors;
        }

        /// <inheritdoc />
        public LearningModelData<IEnumerable<double>, R> FitConvertData
            (IEnumerable<D> data)
        {
            var dataArray = data.ToArray();

            var featuresResult = ProcessFeaturesData(dataArray);
            _removedColumnsIndices = featuresResult.RemovedColumnsIndices;
            var featuresArray = featuresResult.Result.To2dArray();
            CreateScalers(featuresArray);

            var targets = ProcessTargets(dataArray, featuresResult.RemovedRowsIndices);

            return new LearningModelData<IEnumerable<double>, R>(featuresArray, targets);
        }

        /// <summary>
        /// Обрабатывает целевые значения элемента.
        /// </summary>
        /// <param name="item">Элемента.</param>
        /// <returns>Возвращет обработанное целевое значение элемента.</returns>
        protected abstract R ProcessTarget(D item);

        /// <summary>
        /// Обрабатывает признаков данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает обработанные признаков данных.</returns>
        protected abstract IEnumerable<IEnumerable<double?>> ProcessFeatures(IEnumerable<D> data);

        /// <summary>
        /// Создаёт масштабирование данных.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <param name="removedColumnsIndices">Индексы удалённых столбцов.</param>
        /// <returns>Возвращает масштабирование данных.</returns>
        protected abstract IScaler CreateScaler(int index, IEnumerable<int> removedColumnsIndices);

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
        /// Нормализует индексы удалённых индексов.
        /// </summary>
        /// <param name="removedIndicesGroups">Группы индексов удалённых индексов.</param>
        /// <returns>Возвращает нормализованные удалённые индексы.</returns>
        protected IEnumerable<int> NormalizeRemovedIndices
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
                        if (normalizedIndex < removedIndex)
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

        /// <summary>
        /// Обрабатывает целевые значения данных.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="removedRowsIndices">Индексы удалённых строк.</param>
        /// <returns>Возвращает обработанные целевые значения данных.</returns>
        protected IEnumerable<R> ProcessTargets(IEnumerable<D> data,
            IEnumerable<int> removedRowsIndices)
        {
            var index = 0;
            foreach (var taskElement in data)
            {
                if (removedRowsIndices.Contains(index))
                {
                    continue;
                }
                yield return ProcessTarget(taskElement);
                ++index;
            }
        }
    }
}
