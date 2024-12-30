namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс сервиса ресурсов.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Возвращает ресурс по ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>Возвращает ресурс.</returns>
        public object? GetResource(object key);
    }
}
