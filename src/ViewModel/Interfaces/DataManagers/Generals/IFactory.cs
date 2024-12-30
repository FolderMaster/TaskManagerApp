namespace ViewModel.Interfaces.DataManagers.Generals
{
    /// <summary>
    /// Интерфейс для фабрики, создающая объекты.
    /// </summary>
    /// <typeparam name="T">Тип данных создаваемых объектов.</typeparam>
    public interface IFactory<out T>
    {
        /// <summary>
        /// Создает новый экземпляр объекта.
        /// </summary>
        /// <returns>Возвращает новый экземпляр объекта.</returns>
        public T Create();
    }
}
