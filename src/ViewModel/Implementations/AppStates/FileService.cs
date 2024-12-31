using ViewModel.Interfaces.AppStates;

namespace ViewModel.Implementations.AppStates
{
    /// <summary>
    /// Класс файлового сервиса.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IFileService"/>.
    /// </remarks>
    public class FileService : IFileService
    {
        /// <summary>
        /// Путь к персональной директории.
        /// </summary>
        private readonly static string _personalDirectoryPath =
            Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.Personal), "TaskManager");

        /// <inheritdoc/>
        public string PersonalDirectoryPath => _personalDirectoryPath;

        /// <inheritdoc/>
        public Task<byte[]> Load(string path) => File.ReadAllBytesAsync(path);

        /// <inheritdoc/>
        public Task Save(string path, byte[] data) => File.WriteAllBytesAsync(path, data);

        /// <inheritdoc/>
        public Stream CreateStream(string path, FileMode mode,
            FileAccess access, FileShare share) => new FileStream(path, mode, access, share);

        /// <inheritdoc/>
        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        /// <inheritdoc/>
        public string? GetDirectoryPath(string path) => Path.GetDirectoryName(path);

        /// <inheritdoc/>
        public string CombinePath(string path1, string path2) => Path.Combine(path1, path2);

        /// <inheritdoc/>
        public bool IsPathExists(string path) => Path.Exists(path);
    }
}
