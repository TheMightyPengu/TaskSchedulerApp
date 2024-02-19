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

            //modelBuilder.Entity<TasksGiven>().HasOne(u => u.User).WithMany(tg => tg.TasksGiven).HasForeignKey(u => u.Tuid);
            modelBuilder.Entity<TasksGiven>().HasOne(u => u.TGau).WithMany(tg => tg.TasksGiven).HasForeignKey(u => u.TGauid);
            modelBuilder.Entity<TasksGiven>().HasOne(t => t.TGtask).WithMany(tg => tg.TasksGiven).HasForeignKey(t => t.TGtid).OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Reminders>().HasOne(r => r.Rtask).WithMany(t => t.Reminders)..OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Reminders>().HasOne(t => t.Rau).WithMany().HasForeignKey(t => t.Rauid).OnDelete(DeleteBehavior.Restrict);

            ////modelBuilder.Entity<Tasks>().HasMany(t => t.TasksGiven).WithOne(tg => tg.TGtask).HasForeignKey(tg => tg.TGtid).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ApplicationUser>().HasMany(au => au.TasksGiven).WithOne(tg => tg.TGau).HasForeignKey(tg => tg.TGauid).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Tasks>().HasOne(t => t.Tau).WithMany().HasForeignKey(t => t.Tauid).OnDelete(DeleteBehavior.Restrict);

          

        }
    }
}
