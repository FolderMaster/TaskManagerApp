using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    public class FileLogger : ILogger
    {
        private static readonly string _partFilePath = "log.txt";

        public string FilePath { get; set; }

        private readonly object _lock = new();

        private IFileService _fileService;

        public FileLogger(IFileService fileService)
        {
            _fileService = fileService;

            FilePath = _fileService.CombinePath
                (_fileService.PersonalDirectoryPath, _partFilePath);
        }

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
