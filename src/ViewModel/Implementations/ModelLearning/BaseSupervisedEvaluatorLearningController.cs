using MachineLearning;
using MachineLearning.Interfaces.Generals;

using ViewModel.Interfaces.ModelLearning;

namespace ViewModel.Implementations.ModelLearning
{
    /// <summary>
    /// Абстрактный класс базового контроллера обучения модели обучения с учителем.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ILearningController{D, DT, DR}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    /// <typeparam name="D">Тип данных.</typeparam>
    /// <typeparam name="DT">Тип входных данных.</typeparam>
    /// <typeparam name="DR">Тип выходных данных.</typeparam>
    public abstract class BaseSupervisedEvaluatorLearningController<T, R, D, DT, DR> :
        ILearningController<D, DT, DR>
    {
        /// <summary>
        /// Конвертор данных в данные для предсказания с учителем и наоборот.
        /// </summary>
        private ISupervisedLearningConverter<T, R, D, DT, DR> _converter;

        /// <summary>
        /// Модель обучения c учителем.
        /// </summary>
        private ISupervisedLearningModel<T, R> _model;

        /// <summary>
        /// Оценка модели обучения с учителем.
        /// </summary>
        private ISupervisedLearningEvaluator<T, R> _evaluator;

        /// <summary>
        /// Возвращает и задаёт минимальную категорию метрики оценки.
        /// </summary>
        public ScoreMetricCategory MinScoreCategory { get; set; }

        /// <inheritdoc />
        public bool IsValidModel { get; protected set; }

        /// <summary>
        /// Создаёт экземпляр классса
        /// <see cref="BaseSupervisedEvaluatorLearningController{T, R, D, DT, DR}"/>.
        /// </summary>
        /// <param name="converter">
        /// Конвертор данных в данные для предсказания с учителем и наоборот.
        /// </param>
        /// <param name="model">Модель обучения c учителем.</param>
        /// <param name="evaluator">Оценка модели обучения с учителем.</param>
        public BaseSupervisedEvaluatorLearningController
            (ISupervisedLearningConverter<T, R, D, DT, DR> converter,
            ISupervisedLearningModel<T, R> model,
            ISupervisedLearningEvaluator<T, R> evaluator)
        {
            _converter = converter;
            _model = model;
            _evaluator = evaluator;
        }

        /// <inheritdoc />
        public async Task<bool> Train(IEnumerable<D> data)
        {
            if (data == null || !data.Any())
            {
                IsValidModel = false;
                return false;
            }
            var convertedLearningData = _converter.FitConvertData(data);
            _evaluator.Model = _model;
            var scoreCategory = await _evaluator.Evaluate
                (convertedLearningData.Data, convertedLearningData.Targets);
            if (scoreCategory >= MinScoreCategory)
            {
                await _model.Train
                    (convertedLearningData.Data, convertedLearningData.Targets);
                IsValidModel = true;
                return true;
            }
            IsValidModel = false;
            return false;
        }

        /// <inheritdoc />
        public DR Predict(DT data)
        {
            var convertedData = _converter.ConvertData(data);
            var predicted = _model.Predict(convertedData);
            var result = _converter.ConvertPredicted(predicted);
            return result;
        }
    }
}
