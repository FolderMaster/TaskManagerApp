using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.Sessions.Database.Entities
{
    [Table("Tasks")]
    [PrimaryKey(nameof(Id))]
    public class TaskEntity
    {
        public int Id { get; set; }

        public int? ParentTaskId { get; set; }

        [ForeignKey(nameof(ParentTaskId))]
        public virtual TaskCompositeEntity? ParentTask { get; set; }

        public virtual TaskCompositeEntity? TaskComposite { get; set; }

        public virtual TaskElementEntity? TaskElement { get; set; }

        public virtual MetadataEntity Metadata { get; set; }
    }
}
