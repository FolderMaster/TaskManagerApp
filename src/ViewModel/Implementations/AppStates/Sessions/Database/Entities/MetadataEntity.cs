using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using ViewModel.Implementations.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    /// <summary>
    /// Класс сущности метаданных.
    /// </summary>
    [Table("Metadata")]
    [PrimaryKey(nameof(TaskId))]
    public class MetadataEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор задачи.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Возвращает и задаёт название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает и задаёт описание.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Возвращает и задаёт категории.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Возвращает и задаёт задачу.
        /// </summary>
        [ForeignKey(nameof(TaskId))]
        public virtual TaskEntity Task { get; set; }

        /// <summary>
        /// Возвращает и задаёт теги.
        /// </summary>
        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
