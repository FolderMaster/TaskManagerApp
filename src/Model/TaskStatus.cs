namespace Model
{
    /// <summary>
    /// Перечисление статусов задачи.
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// Отменена.
        /// </summary>
        Cancelled,
        /// <summary>
        /// Заблокирована.
        /// </summary>
        Blocked,
        /// <summary>
        /// Отложена.
        /// </summary>
        Deferred,
        /// <summary>
        /// На удержании.
        /// </summary>
        OnHold,
        /// <summary>
        /// В процессе.
        /// </summary>
        InProgress,
        /// <summary>
        /// Запланирована.
        /// </summary>
        Planned,
        /// <summary>
        /// Закрыта.
        /// </summary>
        Closed
    }
}
