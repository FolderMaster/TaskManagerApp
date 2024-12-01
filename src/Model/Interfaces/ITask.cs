namespace Model.Interfaces
{
    /// <summary>
    /// Интерфейс задачи.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Возращает и задаёт родительскую задачу.
        /// </summary>
        public IList<ITask>? ParentTask { get; set; }

        /// <summary>
        /// Возращает и задаёт метаданные.
        /// </summary>
        public object? Metadata { get; set; }

        /// <summary>
        /// Возращает сложность.
        /// </summary>
        public int Difficult { get; }

        /// <summary>
        /// Возращает приоритет.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Возращает статус.
        /// </summary>
        public TaskStatus Status { get; }

        /// <summary>
        /// Возращает срок.
        /// </summary>
        public DateTime? Deadline { get; }

        /// <summary>
        /// Возращает прогресс.
        /// </summary>
        public double Progress { get; }

        /// <summary>
        /// Возращает запланированное время.
        /// </summary>
        public TimeSpan PlannedTime { get; }

        /// <summary>
        /// Возращает потраченное время.
        /// </summary>
        public TimeSpan SpentTime { get; }
    }
}
