using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using ViewModel.Db.Dto;

namespace ViewModel.Db
{
    [Table("Tags")]
    [PrimaryKey(nameof(MetadataId), new string[] { nameof(Tag) })]
    public class TagDto
    {
        public int MetadataId { get; set; }

        public string Tag { get; set; }

        [ForeignKey(nameof(MetadataId))]
        public virtual MetadataDto Metadata { get; set; }
    }
}
