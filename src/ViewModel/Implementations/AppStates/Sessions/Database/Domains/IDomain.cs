namespace ViewModel.Implementations.AppStates.Sessions.Database.Domains
{
    /// <summary>
    /// Индерфейс доменной модели.
    /// </summary>
    public interface IDomain
    {
        /// <summary>
        /// Возвращает индетификатор связанной сущности.
        /// </summary>
        public object EntityId { get; }
    }
}
