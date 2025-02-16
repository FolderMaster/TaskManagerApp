namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения без учителя на данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IUnsupervisedScoreMetric"/>.
    /// </remarks>
    /// <typeparam name="T">Тип предсказанных данных для оценки.</typeparam>
    /// <typeparam name="D">Тип данных для предсказания для оценки.</typeparam>
    public interface IDataUnsupervisedScoreMetric<T, D> : IUnsupervisedScoreMetric
    {
        /// <summary>
        /// Вычисляет оценку на основе предсказанных данных и данных для предсказания.
        /// </summary>
        /// <param name="predicted">Коллекция предсказанных данных.</param>
        /// <param name="data">Коллекция данных для предсказания.</param>
        /// <returns>Возвращает значение оценки.</returns>
        public double CalculateScore(IEnumerable<T> predicted, IEnumerable<D> data);
    }
}
