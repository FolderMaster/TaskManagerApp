namespace MachineLearning.Aggregators
{
    /// <summary>
    /// Класс агрегатора данных по медиане.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="QuantileAggregator"/>.
    /// </remarks>
    public class MedianAggregator : QuantileAggregator
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="MedianAggregator"/> по умолчанию.
        /// </summary>
        public MedianAggregator() : base(0.5) { }
    }
}
