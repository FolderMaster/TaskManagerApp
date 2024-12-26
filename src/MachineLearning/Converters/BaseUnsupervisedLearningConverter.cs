using MachineLearning.Interfaces;
using MachineLearning.Interfaces.Generals;

namespace MachineLearning.Converters
{
    /// <summary>
    /// Абстрактный класс базового конвертора данных в данные
    /// для предсказания без учителя и наоборот.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseLearningConverter{R, D, DT, DR}"/>.
    /// Реализует <see cref="IUnsupervisedLearningConverter{IEnumerable{double}, R, D, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public abstract class BaseUnsupervisedLearningConverter<R, D, DT, DR> :
        BaseLearningConverter<R, D, DT, DR>,
        IUnsupervisedLearningConverter<IEnumerable<double>, R, D, DT, DR>
    {
        /// <summary>
        /// Обработчики точечных данных.
        /// </summary>
        protected readonly IEnumerable<IPointDataProcessor> _pointDataProcessors;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseUnsupervisedLearningConverter{R, D, DT, DR}"/>.
        /// </summary>
        /// <param name="primaryPointDataProcessor">Первичный обработчик точечных данных.</param>
        /// <param name="pointDataProcessors">Обработчики точечных данных.</param>
        protected BaseUnsupervisedLearningConverter
            (IPrimaryPointDataProcessor primaryPointDataProcessor,
            IEnumerable<IPointDataProcessor> pointDataProcessors) :
            base(primaryPointDataProcessor, pointDataProcessors) { }

        /// <inheritdoc />
        public IEnumerable<IEnumerable<double>> FitConvertData(IEnumerable<D> data)
        {
            var dataArray = data.ToArray();

            var featuresResult = ProcessFeaturesData(dataArray);
            _removedColumnsIndices = featuresResult.RemovedColumnsIndices;
            var featuresArray = featuresResult.Result.To2dArray();
            CreateScalers(featuresArray);

            return featuresArray;
        }
    }
}
