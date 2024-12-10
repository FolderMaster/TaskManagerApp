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
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает данные о преобразовании.</returns>
        public DataProcessorResult<R> Process(IEnumerable<T> data);
    }
}
