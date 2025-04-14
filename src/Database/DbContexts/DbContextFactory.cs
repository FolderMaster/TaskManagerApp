using Microsoft.EntityFrameworkCore;

namespace Database.DbContexts
{
    /// <summary>
    /// Класс фабрики, создающая контексты базы данных.
    /// </summary>
    /// <remarks>
    /// Реализует <see cref="IDbContextFactory{BaseDbContext}/>.
    /// </remarks>
    public class DbContextFactory : IDbContextFactory<BaseDbContext>
    {
        /// <inheritdoc/>
        public string ConnectionString { get; set; }

        /// <inheritdoc/>
        public BaseDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            optionsBuilder.UseSqlite(ConnectionString);
            return new BaseDbContext(optionsBuilder.Options);
        } 
    }
}
