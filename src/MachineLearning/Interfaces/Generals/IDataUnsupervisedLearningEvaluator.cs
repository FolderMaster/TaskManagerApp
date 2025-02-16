namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс оценки модели обучения без учителя на данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedLearningEvaluator{T, R}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип входных данных для предсказания.</typeparam>
    /// <typeparam name="R">Тип выходных данных для предсказания.</typeparam>
    public interface IDataUnsupervisedLearningEvaluator<T, R> :
        IUnsupervisedLearningEvaluator<T, R>
    {
        /// <summary>
        /// Возвращает и задаёт метрику оценки обучения с учителем на данных.
        /// </summary>
        public IDataUnsupervisedScoreMetric<R, T> ScoreMetric { get; set; }
    }
}
