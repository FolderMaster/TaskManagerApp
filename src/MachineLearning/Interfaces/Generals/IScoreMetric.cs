namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс метрики оценки.
    /// </summary>
    public interface IScoreMetric
    {
        /// <summary>
        /// Определяет категорию оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        /// <returns>Возвращает категорию оценки.</returns>
        public ScoreMetricCategory GetScoreCategory(double score);
    }
}
