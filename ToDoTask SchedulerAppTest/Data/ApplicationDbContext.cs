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

            modelBuilder.Entity<TasksGiven>().HasKey(tg => new { tg.TGauid, tg.TGtid });
            modelBuilder.Entity<TasksGiven>().HasOne(u => u.TGau).WithMany(tg => tg.TasksGiven).HasForeignKey(u => u.TGauid).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TasksGiven>().HasOne(t => t.TGtask).WithMany(tg => tg.TasksGiven).HasForeignKey(t => t.TGtid).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Reminders>().HasOne(r => r.Rtask).WithMany().HasForeignKey(r => r.Rtid).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Reminders>().HasOne(r => r.Rau).WithMany().HasForeignKey(r => r.Rauid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
