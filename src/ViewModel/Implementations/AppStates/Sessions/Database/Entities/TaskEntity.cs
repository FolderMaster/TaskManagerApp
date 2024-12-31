using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    /// <summary>
    /// Класс сущности задачи.
    /// </summary>
    [Table("Tasks")]
    [PrimaryKey(nameof(Id))]
    public class TaskEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает и задаёт индентификатор родительской задачи.
        /// </summary>
        public int? ParentTaskId { get; set; }

        /// <summary>
        /// Возвращает и задаёт родительскую задачу.
        /// </summary>
        [ForeignKey(nameof(ParentTaskId))]
        public virtual TaskCompositeEntity? ParentTask { get; set; }

        /// <summary>
        /// Возвращает и задаёт составную задачу.
        /// </summary>
        public virtual TaskCompositeEntity? TaskComposite { get; set; }

        /// <summary>
        /// Возвращает и задаёт элементарную задачу.
        /// </summary>
        public virtual TaskElementEntity? TaskElement { get; set; }

        /// <summary>
        /// Возвращает и задаёт метаданные.
        /// </summary>
        public virtual MetadataEntity Metadata { get; set; }
    }
}
