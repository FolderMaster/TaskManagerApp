namespace ViewModel.Implementations.AppStates.Sessions.Database.Mappers
{
    /// <summary>
    /// Интерфейс для преобразования значений между двумя предметными областями.
    /// </summary>
    /// <typeparam name="T1">Тип исходных данных.</typeparam>
    /// <typeparam name="T2">Тип целевых данных.</typeparam>
    public interface IMapper<T1, T2>
    {
        /// <summary>
        /// Преобразует исходные данные в целевые данные.
        /// </summary>
        /// <param name="value">Исходное значение.</param>
        /// <returns>Возвращает целевое значение.</returns>
        public T2 Map(T1 value);

        /// <summary>
        /// Преобразует целевые данные в исходные данные.
        /// </summary>
        /// <param name="value">Целевое значение.</param>
        /// <returns>Возвращает исходное значение.</returns>
        public T1 MapBack(T2 value);
    }
}
