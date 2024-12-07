using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using TaskStatus = Model.TaskStatus;

namespace ViewModel.Implementations.Sessions.Database.Entities
{
    [Table("TaskElements")]
    [PrimaryKey(nameof(Id))]
    public class TaskElementEntity
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
        public virtual TaskEntity Task { get; set; }

        public virtual ICollection<TimeIntervalEntity> TimeIntervals { get; set; }
    }
}
