using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Admins> Admins { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Reminders> Reminders { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<TasksGiven> TasksGiven { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This line is important for configuring Identity tables

            modelBuilder.Entity<TasksGiven>().HasKey(tg => new { tg.Tuid, tg.Ttid });
            modelBuilder.Entity<TasksGiven>().HasOne(t => t.Task).WithMany(tg => tg.TasksGivens).HasForeignKey(t => t.Ttid);
            modelBuilder.Entity<TasksGiven>().HasOne(u => u.User).WithMany(tg => tg.TasksGivens).HasForeignKey(u => u.Tuid);
        }
    }
}
