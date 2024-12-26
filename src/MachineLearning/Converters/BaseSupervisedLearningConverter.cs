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
        /// Создаёт экземпляр класса <see cref="BaseSupervisedLearningConverter{R, D, DT, DR}"/>.
        /// </summary>
        /// <param name="primaryPointDataProcessor">Первичный обработчик точечных данных.</param>
        /// <param name="pointDataProcessors">Обработчики точечных данных.</param>
        protected BaseSupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors) :
            base(primaryPointDataProcessor, pointDataProcessors) { }

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

        /// <summary>
        /// Обрабатывает целевые значения элемента.
        /// </summary>
        /// <param name="item">Элемента.</param>
        /// <returns>Возвращет обработанное целевое значение элемента.</returns>
        protected abstract R ProcessTarget(D item);
    }
}
