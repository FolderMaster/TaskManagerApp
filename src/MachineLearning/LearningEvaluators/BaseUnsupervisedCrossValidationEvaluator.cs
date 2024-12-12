using MachineLearning.Interfaces.Generals;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения без учителя методом кросс-валидации.
    /// Наследует <see cref="BaseCrossValidationLearningEvaluator"/>.
    /// Реализует <see cref="IUnsupervisedLearningEvaluator{T, R}"/>.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public abstract class BaseUnsupervisedCrossValidationEvaluator<T, R> :
        BaseCrossValidationLearningEvaluator, IUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Модель обучения без учителя.
        /// </summary>
        private IUnsupervisedLearningModel<T, R> _model;

        /// <summary>
        /// Метрика оценки для модели обучения без учителя.
        /// </summary>
        private IUnsupervisedScoreMetric<R, T> _scoreMetric;

        /// <summary>
        /// Возвращает и задаёт модель обучения без учителя.
        /// </summary>
        public IUnsupervisedLearningModel<T, R> Model
        {
            get => _model;
            set => UpdateProperty(ref _model, value, OnPropertyChanged);
        }

        /// <summary>
        /// Возвращает и задаёт модель обучения без учителя.
        /// </summary>
        public IUnsupervisedScoreMetric<R, T> ScoreMetric
        {
            get => _scoreMetric;
            set => UpdateProperty(ref _scoreMetric, value, OnPropertyChanged);
        }

        /// <inheritdoc />
        public async Task<ScoreMetricCategory> Evaluate(IEnumerable<T> data)
        {
            var dataArray = data.ToArray();
            var scores = new List<double>();
            foreach (var fold in GetValidationFolds(dataArray))
            {
                var trainData = fold.TrainIndices.Select(i => dataArray[i]);
                var testData = fold.TestIndices.Select(i => dataArray[i]);

                await Model.Train(trainData);
                var predicted = Model.Predict(testData);
                var score = ScoreMetric.CalculateScore(predicted, testData);
                scores.Add(score);
            }
            var scoreCategory = GetScoresCategory(scores);
            return scoreCategory;
        }

        /// <summary>
        /// Возвращает сегменты валидации.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает сегменты валидации.</returns>
        protected abstract IEnumerable<ValidationFold> GetValidationFolds(IEnumerable<T> data);

        /// <summary>
        /// Определяет категорию оценок.
        /// </summary>
        /// <param name="scores">Оценки.</param>
        /// <returns>Возвращает категорию оценок.</returns>
        protected virtual ScoreMetricCategory GetScoresCategory(IEnumerable<double> scores) =>
            ScoreMetric.GetScoreCategory(scores.Average());

        /// <inheritdoc />
        protected override void OnPropertyChanged()
        {
            base.OnPropertyChanged();
            if (Model == null)
            {
                AddError($"{nameof(Model)} должно быть назначено!");
            }
            if (ScoreMetric == null)
            {
                AddError($"{nameof(ScoreMetric)} должно быть назначено!");
            }
        }
    }
}
