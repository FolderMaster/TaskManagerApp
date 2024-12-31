namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
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
        public BaseDbContext Create() => new SqliteDbContext(ConnectionString);
    }
}
