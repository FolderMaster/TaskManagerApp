namespace Database.Domains
{
    /// <summary>
    /// Интерфейс доменной модели.
    /// </summary>
    public interface IDomain
    {
        /// <summary>
        /// Возвращает индетификатор связанной сущности.
        /// </summary>
        public object EntityId { get; }
    }
}
