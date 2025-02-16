using MachineLearning.Interfaces.Generals;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения без учителя методом кросс-валидации
    /// на данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseUnsupervisedCrossValidationEvaluator{T, R}"/>.
    /// Реализует <see cref="IDataUnsupervisedLearningEvaluator{T, R}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public abstract class BaseDataUnsupervisedCrossValidationEvaluator<T, R> :
        BaseUnsupervisedCrossValidationEvaluator<T, R>, IDataUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Метрика оценки для модели обучения без учителя на данных.
        /// </summary>
        private IDataUnsupervisedScoreMetric<R, T> _scoreMetric;

        /// <inheritdoc/>
        public IDataUnsupervisedScoreMetric<R, T> ScoreMetric
        {
            get => _scoreMetric;
            set => UpdateProperty(ref _scoreMetric, value, OnPropertyChanged);
        }

        /// <inheritdoc />
        public override async Task<ScoreMetricCategory> Evaluate(IEnumerable<T> data)
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
            var scoreCategory = ScoreMetric.GetScoreCategory(Aggregator.AggregateToValue(scores));
            return scoreCategory;
        }

        /// <summary>
        /// Создаёт сегменты валидации.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает сегменты валидации.</returns>
        protected abstract IEnumerable<ValidationFold> GetValidationFolds(IEnumerable<T> data);

        /// <inheritdoc />
        protected override void OnPropertyChanged<T>(T oldValue, T newValue)
        {
            base.OnPropertyChanged();
            if (ScoreMetric == null)
            {
                AddError($"{nameof(ScoreMetric)} должно быть назначено!");
            }
        }
    }
}
