using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Db.Dto
{
    [Table("Metadata")]
    [PrimaryKey(nameof(TaskId))]
    public class MetadataDto
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual TaskDto Task { get; set; }

        public virtual ICollection<TagDto> Tags { get; set; }
    }
}
