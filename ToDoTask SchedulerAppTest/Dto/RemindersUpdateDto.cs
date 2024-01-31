using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersUpdateDto
    {
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public int Ruid { get; set; }
        public int Rtid { get; set; }
    }
}