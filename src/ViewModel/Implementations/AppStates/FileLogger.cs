using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    public class FileLogger : ILogger
    {
        private static readonly string _filePath = "log.txt";

        private readonly string _fullFilePath;

        private readonly object _lock = new();

        private IFileService _fileService;

        public FileLogger(IFileService fileService)
        {
            _fileService = fileService;

            _fullFilePath = _fileService.CombinePath
                (_fileService.PersonalDirectoryPath, _filePath);
        }

        public void Log(string message)
        {
            var logMessage = $"{DateTime.Now}: {message}";
            lock (_lock)
            {
                var stream = _fileService.CreateStream(_fullFilePath,
                    FileMode.Append, FileAccess.Write, FileShare.Write);
                var writer = new StreamWriter(stream)
                {
                    AutoFlush = true
                };
                writer.WriteLine(logMessage);
            }
        }
    }
}
