namespace ViewModel.Interfaces.AppStates
{
    /// <summary>
    /// Интерфейс сервиса файлов.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Возвращает путь к персональной директории.
        /// </summary>
        public string PersonalDirectoryPath { get; }

        /// <summary>
        /// Загружает данные из файла по пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <param name="data">Данные.</param>
        /// <returns></returns>
        public Task Save(string path, byte[] data);

        /// <summary>
        /// Загружает данные из файла по пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Возвращает данные.</returns>
        public Task<byte[]> Load(string path);

        /// <summary>
        /// Создаёт поток.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <param name="mode">Режим.</param>
        /// <param name="access">Доступ к использованию этого потока.</param>
        /// <param name="share">Доступ к использованию другим потокам.</param>
        /// <returns>Возвращает поток.</returns>
        public Stream CreateStream(string path, FileMode mode, FileAccess access, FileShare share);

        /// <summary>
        /// Создаёт директорию по пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        public void CreateDirectory(string path);

        /// <summary>
        /// Объединяет пути.
        /// </summary>
        /// <param name="path1">Первый путь.</param>
        /// <param name="path2">Второй путь.</param>
        /// <returns>Возвращает объединённый путь.</returns>
        public string CombinePath(string path1, string path2);

        /// <summary>
        /// Получает путь к директории.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns></returns>
        public string? GetDirectoryPath(string path);

        /// <summary>
        /// Проверяет, существует ли заданный путь.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <returns>Возвращает <c>true</c>, если путь существует, иначе <c>false</c>.</returns>
        public bool IsPathExists(string path);
    }
}
