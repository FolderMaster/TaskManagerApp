namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс оценки модели обучения без учителя.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ILearningEvaluator"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface IUnsupervisedLearningEvaluator<T, R> : ILearningEvaluator
    {
        /// <summary>
        /// Возвращает и задаёт модель обучения без учителя.
        /// </summary>
        public IUnsupervisedLearningModel<T, R> Model { get; set; }

        /// <summary>
        /// Возвращает и задаёт метрику оценки обучения с учителем.
        /// </summary>
        public IUnsupervisedScoreMetric<R, T> ScoreMetric { get; set; }

        /// <summary>
        /// Оценивает модель обучения.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает категорию модели обучения.</returns>
        public Task<ScoreMetricCategory> Evaluate(IEnumerable<T> data);
    }
}
