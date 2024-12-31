using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    /// <summary>
    /// Класс сервиса логгирования сообщений в файл.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="ILogger"/>.
    /// </remarks>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// Часть путь к файлу.
        /// </summary>
        private static readonly string _partFilePath = "log.txt";

        /// <summary>
        /// Возвращает и задаёт путь к файлу.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Объект-заглушка для синхронизации потоков.
        /// </summary>
        private readonly object _lock = new();

        /// <summary>
        /// Файловый сервис.
        /// </summary>
        private IFileService _fileService;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="FileLogger"/>.
        /// </summary>
        /// <param name="fileService">Файловый сервис.</param>
        public FileLogger(IFileService fileService)
        {
            _fileService = fileService;

            FilePath = _fileService.CombinePath
                (_fileService.PersonalDirectoryPath, _partFilePath);
        }

        /// <inheritdoc/>
        public void Log(string message)
        {
            var logMessage = $"{DateTime.Now}: {message}";
            lock (_lock)
            {
                using (var stream = _fileService.CreateStream(FilePath,
                    FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    var writer = new StreamWriter(stream)
                    {
                        AutoFlush = true
                    };
                    writer.WriteLine(logMessage);
                }
            }
        }
    }
}
