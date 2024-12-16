namespace ViewModel.Interfaces.AppStates.Settings
{
    public interface ISettings : IStorageService
    {
        public object Configuration { get; }
    }
}
