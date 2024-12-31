using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
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
        /// Возвращает и задаёт индентификатор элементарной задачи.
        /// </summary>
        public int TaskElementId { get; set; }

        /// <summary>
        /// Возвращает и задаёт элементарную задачу.
        /// </summary>
        [ForeignKey(nameof(TaskElementId))]
        public virtual TaskElementEntity TaskElement { get; set; }
    }
}
