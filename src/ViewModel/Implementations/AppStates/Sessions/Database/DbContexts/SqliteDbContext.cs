using Microsoft.EntityFrameworkCore;

namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
{
    public class SqliteDbContext : BaseDbContext
    {
        public SqliteDbContext(string connectionString) : base(connectionString) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
