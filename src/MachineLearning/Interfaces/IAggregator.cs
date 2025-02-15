namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс для агрегирования данных.
    /// </summary>
    public interface IAggregator
    {
        /// <summary>
        /// Агрегирует набор данных в значение.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Агрегированное значение.</returns>
        public double AggregateToValue(IEnumerable<double> data);
    }
}
