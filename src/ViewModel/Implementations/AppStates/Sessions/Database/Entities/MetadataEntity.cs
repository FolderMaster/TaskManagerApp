using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using ViewModel.Implementations.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    [Table("Metadata")]
    [PrimaryKey(nameof(TaskId))]
    public class MetadataEntity
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual TaskEntity Task { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
    }
}
