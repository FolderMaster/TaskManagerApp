using ViewModel.Interfaces;

namespace ViewModel.Implementations
{
    public class FileService : IFileService
    {
        public string PersonalDirectoryPath =>
            Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public byte[] Load(string path) => File.ReadAllBytes(path);

        public void Save(string path, byte[] data) => File.WriteAllBytes(path, data);

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);

        public string? GetDirectoryPath(string path) => Path.GetDirectoryName(path);

        public string CombinePath(string path1, string path2) => Path.Combine(path1, path2);

        public bool IsPathExists(string path) => Path.Exists(path);
    }
}
