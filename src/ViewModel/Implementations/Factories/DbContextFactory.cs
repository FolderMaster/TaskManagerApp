using ViewModel.Implementations.AppStates.Sessions.Database.DbContexts;
using ViewModel.Interfaces;

namespace ViewModel.Implementations.Factories
{
    public class DbContextFactory : IFactory<BaseDbContext>
    {
        public BaseDbContext Create() => new SqliteDbContext();
    }
}
