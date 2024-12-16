using Microsoft.EntityFrameworkCore;

using ViewModel.Implementations.AppStates.Sessions.Database.Entities;
using ViewModel.Implementations.Sessions.Database.Entities;

namespace ViewModel.Implementations.AppStates.Sessions.Database.DbContexts
{
    public class BaseDbContext : DbContext
    {
        protected string _connectionString;

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<MetadataEntity> Metadata { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<TaskCompositeEntity> TaskComposites { get; set; }

        public DbSet<TaskElementEntity> TaskElements { get; set; }

        public DbSet<TimeIntervalEntity> TimeIntervals { get; set; }

        public BaseDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>().HasOne(t => t.ParentTask).
                WithMany(tc => tc.Subtasks).HasForeignKey(t => t.ParentTaskId).
                OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
