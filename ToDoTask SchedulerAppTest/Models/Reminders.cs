using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class Reminders
    {
        [Key]
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public Users Ruid { get; set; }
        public Tasks Rtid { get; set; }
    }
}
