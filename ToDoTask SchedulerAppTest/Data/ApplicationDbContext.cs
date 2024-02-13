using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public DbSet<Admins> Admins { get; set; }
        //public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Reminders> Reminders { get; set; }
        //public DbSet<Logs> Logs { get; set; }
        public DbSet<TasksGiven> TasksGiven { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TasksGiven>().HasKey(tg => new { tg.Tauid, tg.Ttid });
            modelBuilder.Entity<TasksGiven>().HasOne(t => t.Task).WithMany(tg => tg.TasksGiven).HasForeignKey(t => t.Ttid);
            //modelBuilder.Entity<TasksGiven>().HasOne(u => u.User).WithMany(tg => tg.TasksGiven).HasForeignKey(u => u.Tuid);
            modelBuilder.Entity<TasksGiven>().HasOne(u => u.ApplicationUser).WithMany(tg => tg.TasksGiven).HasForeignKey(u => u.Tauid);
        }
    }
}
