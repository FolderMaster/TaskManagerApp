namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс оценки модели обучения без учителя на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedLearningEvaluator{T, R}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface IPredictedUnsupervisedLearningEvaluator<T, R> :
        IUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Возвращает и задаёт метрику оценки обучения с учителем на предсказанных данных.
        /// </summary>
        public IPredictedUnsupervisedScoreMetric<R> ScoreMetric { get; set; }
    }
}
