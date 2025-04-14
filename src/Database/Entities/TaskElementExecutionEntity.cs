using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using TaskStatus = Model.TaskStatus;

namespace Database.Entities
{
    /// <summary>
    /// Класс сущности выполнения элементарной задачи.
    /// </summary>
    [Table("TaskElementExecutions")]
    [PrimaryKey(nameof(Id))]
    public class TaskElementExecutionEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает и задаёт прогресс.
        /// </summary>
        public double Progress { get; set; }

        /// <summary>
        /// Возвращает и задаёт статус.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Возвращает и задаёт потраченное время.
        /// </summary>
        public TimeSpan SpentTime { get; set; }

        /// <summary>
        /// Возвращает и задаёт выполненный реальный показатель.
        /// </summary>
        public double ExecutedReal { get; set; }

        /// <summary>
        /// Возращает дату создания.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор элементарной задачи.
        /// </summary>
        public int TaskElementId { get; set; }

        /// <summary>
        /// Возвращает и задаёт элементарную задачу.
        /// </summary>
        [ForeignKey(nameof(TaskElementId))]
        public virtual TaskElementEntity TaskElement { get; set; }

        /// <summary>
        /// Возвращает и задаёт временные интервалы.
        /// </summary>
        public virtual ICollection<TimeIntervalEntity> TimeIntervals { get; set; }
    }
}
