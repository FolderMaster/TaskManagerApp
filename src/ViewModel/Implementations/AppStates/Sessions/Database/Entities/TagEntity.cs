using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.Sessions.Database.Entities
{
    /// <summary>
    /// Класс сущности тега.
    /// </summary>
    [Table("Tags")]
    [PrimaryKey(nameof(MetadataId), new string[] { nameof(Tag) })]
    public class TagEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентифиактор метаданных.
        /// </summary>
        public int MetadataId { get; set; }

        /// <summary>
        /// Возвращает и задаёт тег.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Возвращает и задаёт метаданные.
        /// </summary>
        [ForeignKey(nameof(MetadataId))]
        public virtual MetadataEntity Metadata { get; set; }
    }
}
