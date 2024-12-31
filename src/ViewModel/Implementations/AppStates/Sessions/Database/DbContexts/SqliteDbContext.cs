using Microsoft.EntityFrameworkCore;

namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
{
    /// <summary>
    /// Класс контекста базы данных SQLite.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="BaseDbContext"/>.
    /// </remarks>
    public class SqliteDbContext : BaseDbContext
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="SqliteDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public SqliteDbContext(string connectionString) : base(connectionString) { }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
