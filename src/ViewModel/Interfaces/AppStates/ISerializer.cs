namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс сериализатора.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Сериализует объект.
        /// </summary>
        /// <param name="value">Объект.</param>
        /// <returns>Возвращает массив байтов, представляющий сериализованные данные.</returns>
        byte[] Serialize(object value);

        /// <summary>
        /// Десериализует объект.
        /// </summary>
        /// <typeparam name="T">Тип данных.</typeparam>
        /// <param name="data">Данные.</param>
        /// <returns>Возвращает десериализованный объект.</returns>
        T? Deserialize<T>(byte[] data);
    }
}
