using Microsoft.EntityFrameworkCore;
using ViewModel.Db.Dto;

namespace ViewModel.Db
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<TaskDto> Tasks { get; set; }

        public DbSet<MetadataDto> Metadata { get; set; }

        public DbSet<TagDto> Tags { get; set; }

        public DbSet<TaskCompositeDto> TaskComposites { get; set; }

        public DbSet<TaskElementDto> TaskElements { get; set; }

        public DbSet<TimeIntervalDto> TimeIntervals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TaskManager.db3");
        }

        protected override void ConfigureConventions
            (ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskDto>().HasOne(t => t.ParentTask).
                WithMany(tc => tc.Subtasks).HasForeignKey(t => t.ParentTaskId);
        }
    }
}
