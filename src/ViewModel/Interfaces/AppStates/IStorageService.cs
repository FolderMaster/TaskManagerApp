namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс для сервиса хранилища.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Сохраняет данные в хранилище.
        /// </summary>
        /// <returns>Возвращает задачц процесса сохранения данных.</returns>
        public Task Save();

        /// <summary>
        /// Загружает данные из хранилища.
        /// </summary>
        /// <returns>Возвращает задачц процесса загрузки данных.</returns>
        public Task Load();
    }
}
