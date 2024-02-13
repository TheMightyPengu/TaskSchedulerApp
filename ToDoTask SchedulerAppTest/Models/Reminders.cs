using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    //public class Reminders
    //{
    //    [Key]
    //    public int Rid { get; set; }
    //    public DateTime ReminderDate { get; set; }
    //    public Users Ruid { get; set; }
    //    public Tasks Rtid { get; set; }
    //}
    public class Reminders
    {
        [Key]
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Rauid { get; set; }
        public virtual ApplicationUser Rau { get; set; }
        public int Rtid { get; set; }
        public virtual Tasks Rtask { get; set; }
    }
}
