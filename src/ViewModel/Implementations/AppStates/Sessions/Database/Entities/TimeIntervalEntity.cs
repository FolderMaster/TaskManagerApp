using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    [Table("TimeIntervals")]
    [PrimaryKey(nameof(Id))]
    public class TimeIntervalEntity
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TaskElementId { get; set; }

        [ForeignKey(nameof(TaskElementId))]
        public virtual TaskElementEntity TaskElement { get; set; }
    }
}
