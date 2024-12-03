using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Db.Dto
{
    [Table("TimeIntervals")]
    [PrimaryKey(nameof(Id))]
    public class TimeIntervalDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TaskElementId { get; set; }

        [ForeignKey(nameof(TaskElementId))]
        public virtual TaskElementDto TaskElement { get; set; }
    }
}
