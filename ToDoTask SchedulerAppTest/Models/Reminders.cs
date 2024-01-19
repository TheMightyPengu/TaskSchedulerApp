using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class Reminders
    {
        [Key]
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public Users Ruid { get; set; }//still dont know if thats correct
        public Tasks Rtid { get; set; }//still dont know if thats correct
    }
}
