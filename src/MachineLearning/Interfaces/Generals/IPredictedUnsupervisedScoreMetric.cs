namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения без учителя на предсказанных данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedScoreMetric"/>.
    /// </remarks>
    /// <typeparam name="T">Тип предсказанных данных для оценки.</typeparam>
    public interface IPredictedUnsupervisedScoreMetric<T> : IUnsupervisedScoreMetric
    {
        /// <summary>
        /// Вычисляет оценку на основе предсказанных данных.
        /// </summary>
        /// <param name="predicted1">Первая коллекция предсказанных данных.</param>
        /// <param name="predicted2">Вторая коллекция предсказанных данных.</param>
        /// <returns>Возвращает значение оценки.</returns>
        public double CalculateScore(IEnumerable<T> predicted1, IEnumerable<T> predicted2);
    }
}
