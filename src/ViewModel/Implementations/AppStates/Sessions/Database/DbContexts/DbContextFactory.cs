using ViewModel.Interfaces.AppStates.Sessions;

namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
{
    public class DbContextFactory : IDbContextFactory<BaseDbContext>
    {
        public string ConnectionString { get; set; }

        public BaseDbContext Create() => new SqliteDbContext(ConnectionString);
    }
}
