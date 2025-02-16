using MachineLearning.Interfaces.Generals;

namespace MachineLearning.LearningEvaluators
{
    /// <summary>
    /// Абстрактный класс базовой оценки модели обучения без учителя методом кросс-валидации
    /// на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseUnsupervisedCrossValidationEvaluator{T, R}"/>.
    /// Реализует <see cref="IPredictedUnsupervisedLearningEvaluator{T, R}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public abstract class BasePredictedUnsupervisedCrossValidationEvaluator<T, R> :
        BaseUnsupervisedCrossValidationEvaluator<T, R>,
        IPredictedUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Метрика оценки для модели обучения без учителя на предсказанных данных.
        /// </summary>
        private IPredictedUnsupervisedScoreMetric<R> _scoreMetric;

        /// <inheritdoc/>
        public IPredictedUnsupervisedScoreMetric<R> ScoreMetric
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
                var testData = fold.TestIndices.Select(i => dataArray[i]);
                var predictedValues = new List<R[]>();
                foreach (var secondaryTrainIndices in GetSecondaryTrainIndices(fold.TrainIndices))
                {
                    var trainData = secondaryTrainIndices.Select(i => dataArray[i]);
                    await Model.Train(trainData);
                    var predicted = Model.Predict(testData).ToArray();
                    predictedValues.Add(predicted);
                }
                var predictedValuesCount = predictedValues.Count;
                for (var i = 1; i < predictedValuesCount; ++i)
                {
                    var score = ScoreMetric.CalculateScore
                        (predictedValues[i - 1], predictedValues[i]);
                    scores.Add(score);
                }
            }
            var scoreCategory = ScoreMetric.GetScoreCategory(Aggregator.AggregateToValue(scores));
            return scoreCategory;
        }

        /// <summary>
        /// Создаёт сегменты валидации.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает сегменты валидации.</returns>
        protected abstract IEnumerable<ValidationFold> GetValidationFolds
            (IEnumerable<T> data);

        /// <summary>
        /// Создаёт вторичные индексы тренировочных данных.
        /// </summary>
        /// <param name="trainIndices">Индексы тренировочных данных.</param>
        /// <returns>Возвращает вторичные индексы тренировочных данных.</returns>
        protected abstract IEnumerable<IEnumerable<int>> GetSecondaryTrainIndices
            (IEnumerable<int> trainIndices);

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
