using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    [Table("TaskComposites")]
    [PrimaryKey(nameof(Id))]
    public class TaskCompositeEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual TaskEntity Task { get; set; }

        public virtual ICollection<TaskEntity> Subtasks { get; set; } = new List<TaskEntity>();
    }
}
