namespace ViewModel.Interfaces.AppStates
{
    public interface IFileService
    {
        public string PersonalDirectoryPath { get; }

        public Task Save(string path, byte[] data);

        public Task<byte[]> Load(string path);

        public Stream CreateStream(string path, FileMode mode, FileAccess access, FileShare share);

        public void CreateDirectory(string path);

        public string CombinePath(string path1, string path2);

        public string? GetDirectoryPath(string path);

        public bool IsPathExists(string path);
    }
}
