using MachineLearning.Interfaces.Generals;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения с учителем методом кросс-валидации.
    /// Наследует <see cref="BaseCrossValidationLearningEvaluator"/>.
    /// Реализует <see cref="ISupervisedLearningEvaluator{T, R}"/>.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public abstract class BaseSupervisedCrossValidationEvaluator<T, R> :
        BaseCrossValidationLearningEvaluator, ISupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Модель обучения с учителем.
        /// </summary>
        private ISupervisedLearningModel<T, R> _model;

        /// <summary>
        /// Метрика оценки для модели обучения с учителем.
        /// </summary>
        private ISupervisedScoreMetric<R> _scoreMetric;

        /// <summary>
        /// Возвращает и задаёт модель обучения с учителем.
        /// </summary>
        public ISupervisedLearningModel<T, R> Model
        {
            get => _model;
            set => UpdateProperty(ref _model, value, OnPropertyChanged);
        }

        /// <summary>
        /// Возвращает и задаёт модель обучения с учителем.
        /// </summary>
        public ISupervisedScoreMetric<R> ScoreMetric
        {
            get => _scoreMetric;
            set => UpdateProperty(ref _scoreMetric, value, OnPropertyChanged);
        }

        /// <inheritdoc />
        public async Task<ScoreMetricCategory> Evaluate
            (IEnumerable<T> data, IEnumerable<R> targets)
        {
            var dataArray = data.ToArray();
            var targetsArray = targets.ToArray();
            var scores = new List<double>();
            foreach (var fold in GetValidationFolds(dataArray, targets))
            {
                var trainData = fold.TrainIndices.Select(i => dataArray[i]);
                var trainTarget = fold.TrainIndices.Select(i => targetsArray[i]);
                var testData = fold.TestIndices.Select(i => dataArray[i]);
                var testTargets = fold.TestIndices.Select(i => targetsArray[i]);

                await Model.Train(trainData, trainTarget);
                var predicted = Model.Predict(testData);
                var score = ScoreMetric.CalculateScore(testTargets, predicted);
                scores.Add(score);
            }
            var scoreCategory = GetScoresCategory(scores);
            return scoreCategory;
        }

        /// <summary>
        /// Возвращает сегменты валидации.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="targets">Целевые значения.</param>
        /// <returns>Возвращает сегменты валидации.</returns>
        protected abstract IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<T> data, IEnumerable<R> targets);

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
