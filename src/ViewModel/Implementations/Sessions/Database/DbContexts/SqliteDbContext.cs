using Microsoft.EntityFrameworkCore;

namespace ViewModel.Implementations.Sessions.Database.DbContexts
{
    public class SqliteDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=TaskManager.db3");
        }
    }
}
