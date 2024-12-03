using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Db.Dto
{
    [Table("TaskElements")]
    [PrimaryKey(nameof(Id))]
    public class TaskElementDto
    {
        public int Id { get; set; }

        public int Difficult { get; set; }

        public int Priority { get; set; }

        public DateTime? Deadline { get; set; }

        public TaskStatus Status { get; set; }

        public double Progress { get; set; }

        public TimeSpan PlannedTime { get; set; }

        public TimeSpan SpentTime { get; set; }

        public double PlannedReal { get; set; }

        public double ExecutedReal { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual TaskDto Task { get; set; }

        public virtual ICollection<TimeIntervalDto> TimeIntervals { get; set; }
    }
}
