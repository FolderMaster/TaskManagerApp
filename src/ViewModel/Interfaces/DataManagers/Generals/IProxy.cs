namespace ViewModel.Interfaces.DataManagers.Generals
{
    /// <summary>
    /// Интерфейс заместителя.
    /// </summary>
    /// <typeparam name="T">Тип заменяемого данных.</typeparam>
    public interface IProxy<out T>
    {
        /// <summary>
        /// Возвращает заменяемого объекта.
        /// </summary>
        public T Target { get; }
    }
}
