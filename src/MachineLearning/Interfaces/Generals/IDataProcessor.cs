namespace MachineLearning.Interfaces.Generals
{
    /// <summary>
    /// Интерфейс обработчика данных.
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    /// <typeparam name="R">Тип выходных данных.</typeparam>
    public interface IDataProcessor<T, R>
    {
        /// <summary>
        /// Преобразует данные.
        /// </summary>
        /// <param name="data">Коллекция данных.</param>
        /// <returns>Возвращает коллекцию преобразованных данных.</returns>
        public IEnumerable<IEnumerable<R>> Process(IEnumerable<IEnumerable<T>> data);
    }
}
