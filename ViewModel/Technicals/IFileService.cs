namespace ViewModel.Technicals
{
    public interface IFileService
    {
        public string PersonalDirectoryPath { get; }

        public void Save(string path, byte[] data);

        public byte[] Load(string path);

        public void CreateDirectory(string path);

        public string CombinePath(string path1, string path2);

        public string? GetDirectoryPath(string path);

        public bool IsPathExists(string path);
    }
}
