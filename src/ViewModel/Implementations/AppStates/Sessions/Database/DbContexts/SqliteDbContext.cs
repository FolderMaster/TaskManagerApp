using Microsoft.EntityFrameworkCore;

namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
{
    public class SqliteDbContext : BaseDbContext
    {
        private readonly string _connectionString;

        public SqliteDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
