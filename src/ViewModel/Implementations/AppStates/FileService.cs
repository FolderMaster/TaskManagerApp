using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    public class FileService : IFileService
    {
        private readonly static string _personalDirectoryPath =
            Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "TaskManager");

        public string PersonalDirectoryPath => _personalDirectoryPath;

        public Task<byte[]> Load(string path) => File.ReadAllBytesAsync(path);

        public Task Save(string path, byte[] data) => File.WriteAllBytesAsync(path, data);

        public Stream CreateStream(string path, FileMode mode,
            FileAccess access, FileShare share) => new FileStream(path, mode, access, share);

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public string? GetDirectoryPath(string path) => Path.GetDirectoryName(path);

        public string CombinePath(string path1, string path2) => Path.Combine(path1, path2);

        public bool IsPathExists(string path) => Path.Exists(path);
    }
}
