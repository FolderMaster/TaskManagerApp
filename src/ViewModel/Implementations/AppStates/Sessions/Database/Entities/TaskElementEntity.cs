using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    /// <summary>
    /// Класс сущности элементарной задачи.
    /// </summary>
    [Table("TaskElements")]
    [PrimaryKey(nameof(Id))]
    public class TaskElementEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает и задаёт сложность.
        /// </summary>
        public int Difficult { get; set; }

        /// <summary>
        /// Возвращает и задаёт приоритет.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Возвращает и задаёт срока.
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Возвращает и задаёт статус.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Возвращает и задаёт прогресс.
        /// </summary>
        public double Progress { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированное времени.
        /// </summary>
        public TimeSpan PlannedTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт проведённое времени.
        /// </summary>
        public TimeSpan SpentTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт запланированный реальный показатель.
        /// </summary>
        public double PlannedReal { get; set; }

        /// <summary>
        /// Возвращает и задаёт выполненный реальный показатель.
        /// </summary>
        public double ExecutedReal { get; set; }

        /// <summary>
        /// Возвращает и задаёт задачу.
        /// </summary>
        [ForeignKey(nameof(Id))]
        public virtual TaskEntity Task { get; set; }

        /// <summary>
        /// Возвращает и задаёт временные интервалы.
        /// </summary>
        public virtual ICollection<TimeIntervalEntity> TimeIntervals { get; set; }
    }
}
