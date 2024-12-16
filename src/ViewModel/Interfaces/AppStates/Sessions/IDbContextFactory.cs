using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Interfaces.AppStates.Sessions
{
    public interface IDbContextFactory<T> : IFactory<T>
    {
        public string ConnectionString { get; set; }
    }
}
