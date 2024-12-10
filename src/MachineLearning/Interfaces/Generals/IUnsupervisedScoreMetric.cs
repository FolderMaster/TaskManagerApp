namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики оценки для модели обучения без учителя.
    /// Наследует <see cref="IScoreMetric"/>.
    /// </summary>
    /// <typeparam name="T">Тип фактических данных для оценки.</typeparam>
    /// <typeparam name="D">Тип данных для предсказания для оценки.</typeparam>
    public interface IUnsupervisedScoreMetric<T, D> : IScoreMetric
    {
        /// <summary>
        /// Вычисляет оценку на основе фактических данных и данных для предсказания.
        /// </summary>
        /// <param name="actual">Коллекция фактических данных.</param>
        /// <param name="data">Коллекция данных для предсказания.</param>
        /// <returns>Возвращает значение оценки.</returns>
        public double CalculateScore(IEnumerable<T> actual, IEnumerable<D> data);
    }
}
