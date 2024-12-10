namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения с учителем.
    /// Наследует <see cref="IScoreMetric"/>.
    /// </summary>
    /// <typeparam name="T">Тип данных для оценки.</typeparam>
    public interface ISupervisedScoreMetric<T> : IScoreMetric
    {
        /// <summary>
        /// Вычисляет оценку на основе фактических и предсказанных данных.
        /// </summary>
        /// <param name="actual">Коллекция фактических данных.</param>
        /// <param name="predicted">Коллекция предсказанных значений.</param>
        /// <returns>Возвращает значение оценки.</returns>
        public double CalculateScore(IEnumerable<T> actual, IEnumerable<T> predicted);
    }
}
