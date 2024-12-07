namespace ViewModel.Interfaces
{
    public interface IStorageService
    {
        public Task Save();

        public Task Load();
    }
}
