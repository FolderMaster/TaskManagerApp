namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс элементарной задачи.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="ITask"/>.
    /// </remarks>
    public interface ITaskElement : ITask
    {
        /// <summary>
        /// Возвращает и задаёт сложность.
        /// </summary>
        public new int Difficult { get; set; }

        /// <summary>
        /// Возвращает и задаёт приоритет.
        /// </summary>
        public new int Priority { get; set; }

        /// <summary>
        /// Возвращает и задаёт срок.
        /// </summary>
        public new DateTime? Deadline { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированное время.
        /// </summary>
        public new TimeSpan PlannedTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированный реальный показатель.
        /// </summary>
        public double PlannedReal { get; set; }

        /// <summary>
        /// Возвращает выполнения элементарной задачи.
        /// </summary>
        public IEnumerable<ITaskElementExecution> Executions { get; }
    }
}
