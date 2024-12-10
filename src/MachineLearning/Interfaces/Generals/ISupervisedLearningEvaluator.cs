namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс оценки модели обучения с учителем. Наследует <see cref="ILearningEvaluator"/>.
    /// </summary>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface ISupervisedLearningEvaluator<T, R> : ILearningEvaluator
    {
        /// <summary>
        /// Возвращает и задаёт модель обучения с учителем.
        /// </summary>
        public ISupervisedLearningModel<T, R> Model { get; set; }

        /// <summary>
        /// Возвращает и задаёт метрику оценки обучения с учителем.
        /// </summary>
        public ISupervisedScoreMetric<R> ScoreMetric { get; set; }

        /// <summary>
        /// Оценивает модель обучения.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="targets">Целевые значения.</param>
        /// <returns>Возвращает категорию модели обучения.</returns>
        public Task<ScoreMetricCategory> Evaluate(IEnumerable<T> data, IEnumerable<R> targets);
    }
}
