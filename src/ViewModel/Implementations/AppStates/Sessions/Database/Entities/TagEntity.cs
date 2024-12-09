using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;

namespace ViewModel.Implementations.Sessions.Database.Entities
{
    [Table("Tags")]
    [PrimaryKey(nameof(MetadataId), new string[] { nameof(Tag) })]
    public class TagEntity
    {
        public int MetadataId { get; set; }

        public string Tag { get; set; }

        [ForeignKey(nameof(MetadataId))]
        public virtual MetadataEntity Metadata { get; set; }
    }
}
