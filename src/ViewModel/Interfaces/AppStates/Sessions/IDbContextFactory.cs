using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.AppStates.Sessions
{
    /// <summary>
    /// Интерфейс фабрики, создающая контексты базы данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="IFactory{T}"/>.
    /// </remarks>
    /// <typeparam name="T">Тип данных.</typeparam>
    public interface IDbContextFactory<T> : IFactory<T>
    {
        /// <summary>
        /// Возвращает и задаёт строку подключения.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
