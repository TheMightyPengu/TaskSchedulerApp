using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersCreateDto
    {
        public DateTime ReminderDate { get; set; }
        public string Rauid { get; set; }
        public int Rtid { get; set; }
    }

}