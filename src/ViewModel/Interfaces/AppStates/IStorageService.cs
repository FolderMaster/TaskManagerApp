namespace ViewModel.Interfaces.AppStates
{
    public interface IStorageService
    {
        public Task Save();

        public Task Load();
    }
}
