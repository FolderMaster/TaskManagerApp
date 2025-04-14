using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    /// <summary>
    /// Класс сущности временных интервалов.
    /// </summary>
    [Table("TimeIntervals")]
    [PrimaryKey(nameof(Id))]
    public class TimeIntervalEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает и задаёт начало.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Возвращает и задаёт конец.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор выполнения элементарной задачи.
        /// </summary>
        public int TaskElementExecutionId { get; set; }

        /// <summary>
        /// Возвращает и задаёт выполнение элементарной задачи.
        /// </summary>
        [ForeignKey(nameof(TaskElementExecutionId))]
        public virtual TaskElementExecutionEntity TaskElementExecution { get; set; }
    }
}
