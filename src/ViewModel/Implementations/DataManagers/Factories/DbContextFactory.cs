using ViewModel.Implementations.AppStates.Sessions.Database.DbContexts;
using ViewModel.Interfaces.DataManagers.Generals;

namespace ViewModel.Implementations.DataManagers.Factories
{
    public class DbContextFactory : IFactory<BaseDbContext>
    {
        public BaseDbContext Create() => new SqliteDbContext
            ($"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + "TaskManager.db3"}");
    }
}
