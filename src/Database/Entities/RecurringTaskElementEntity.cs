using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    /// <summary>
    /// Класс сущности повторяющейся элементарной задачи.
    /// </summary>
    [Table("RecurringTaskElements")]
    [PrimaryKey(nameof(Id))]
    public class RecurringTaskElementEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает настройку повторения.
        /// </summary>
        public RecurringSettingsOwnedEntity RecurringSettings { get; set; }

        /// <summary>
        /// Возвращает последняя дата обновления выполнений.
        /// </summary>
        public DateTime LastUpdatedExecutionsDate { get; set; }

        /// <summary>
        /// Возвращает и задаёт элементарную задачу.
        /// </summary>
        [ForeignKey(nameof(Id))]
        public virtual TaskElementEntity TaskElement { get; set; }
    }
}
