using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel.Implementations.AppStates.Sessions.Database.Entities
{
    /// <summary>
    /// Класс сущности составной задачи.
    /// </summary>
    [Table("TaskComposites")]
    [PrimaryKey(nameof(Id))]
    public class TaskCompositeEntity
    {
        /// <summary>
        /// Возвращает и задаёт индентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает и задаёт задачи.
        /// </summary>
        [ForeignKey(nameof(Id))]
        public virtual TaskEntity Task { get; set; }

        /// <summary>
        /// Возвращает и задаёт подзадачи.
        /// </summary>
        public virtual ICollection<TaskEntity> Subtasks { get; set; } = new List<TaskEntity>();
    }
}
