using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Db.Dto
{
    [Table("TaskComposites")]
    [PrimaryKey(nameof(Id))]
    public class TaskCompositeDto
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual TaskDto Task { get; set; }

        public virtual ICollection<TaskDto> Subtasks { get; set; }
    }
}
