using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Db.Dto
{
    [Table("Tasks")]
    [PrimaryKey(nameof(Id))]
    public class TaskDto
    {
        public int Id { get; set; }

        public int? ParentTaskId { get; set; }

        [ForeignKey(nameof(ParentTaskId))]
        public virtual TaskCompositeDto ParentTask { get; set; }

        // public virtual TaskCompositeDto? TaskComposite { get; set; }

        //public virtual TaskElementDto? TaskElement { get; set; }

        public virtual MetadataDto Metadata { get; set; }
    }
}
