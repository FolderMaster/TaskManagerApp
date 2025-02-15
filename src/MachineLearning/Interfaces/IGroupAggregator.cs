namespace MachineLearning.Interfaces
{
    /// <summary>
    /// Интерфейс для агрегирования данных в группу.
    /// </summary>
    public interface IGroupAggregator
    {
        /// <summary>
        /// Агрегирует набор данных в группу.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Агрегированную группу.</returns>
        public IEnumerable<double> AggregateToGroup(IEnumerable<double> data);
    }
}
