using Microsoft.EntityFrameworkCore;

using Database.Entities;

namespace Database.DbContexts
{
    /// <summary>
    /// Базовый класс контекста базы данных.
    /// </summary>
    /// <remarks>
    /// Наследует <see cref="DbContext"/>.
    /// </remarks>
    public class BaseDbContext : DbContext
    {
        /// <summary>
        /// Возвращает и задаёт сущности задачи.
        /// </summary>
        public DbSet<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности метаданных.
        /// </summary>
        public DbSet<MetadataEntity> Metadata { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности тега.
        /// </summary>
        public DbSet<TagEntity> Tags { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности составной задачи.
        /// </summary>
        public DbSet<TaskCompositeEntity> TaskComposites { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности элементарной задачи.
        /// </summary>
        public DbSet<TaskElementEntity> TaskElements { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности выполнения элементарной задачи.
        /// </summary>
        public DbSet<TaskElementExecutionEntity> TaskElementExecutions { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности временного интерала.
        /// </summary>
        public DbSet<TimeIntervalEntity> TimeIntervals { get; set; }

        /// <summary>
        /// Возвращает и задаёт сущности повторяющейся элементарной задачи.
        /// </summary>
        public DbSet<RecurringTaskElementEntity> RecurringTaskElements { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="BaseDbContext"/>.
        /// </summary>
        /// <param name="options">Настройки.</param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>().HasOne(t => t.ParentTask).
                WithMany(tc => tc.Subtasks).HasForeignKey(t => t.ParentTaskId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
