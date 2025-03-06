namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс повторяющейся элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITaskElement"/>.
    /// </remarks>
    public interface IRecurringTaskElement : ITaskElement
    {
        /// <summary>
        /// Возвращает настройку повторения.
        /// </summary>
        public RecurringSettings RecurringSettings { get; }

        /// <summary>
        /// Возвращает последняя дата обновления выполнений.
        /// </summary>
        public DateTime LastUpdatedExecutionsDate { get; }

        /// <summary>
        /// Обновляет выполнения задач <see cref="ITaskElement.Executions"/>.
        /// </summary>
        public void UpdateExecutions();
    }
}
