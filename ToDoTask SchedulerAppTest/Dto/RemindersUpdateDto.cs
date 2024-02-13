using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersUpdateDto
    {
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Rauid { get; set; }
        public int Rtid { get; set; }
    }
}