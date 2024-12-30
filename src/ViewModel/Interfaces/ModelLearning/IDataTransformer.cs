namespace ViewModel.Interfaces.ModelLearning
{
    /// <summary>
    /// Интерфейс для преобразования данных из одного типа в другой.
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    /// <typeparam name="R">Тип выходных данных.</typeparam>
    public interface IDataTransformer<T, R>
    {
        /// <summary>
        /// Адаптирует преобразование под данные.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает преобразованные данные.</returns>
        IEnumerable<R> FitTransform(IEnumerable<T> data);

        /// <summary>
        /// Преобразует данные.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает преобразованные данные.</returns>
        R Transform(T data);
    }
}
