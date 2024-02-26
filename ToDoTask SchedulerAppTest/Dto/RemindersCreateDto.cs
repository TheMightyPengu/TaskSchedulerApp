using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersCreateDto
    {
        public string Rauid { get; set; }
        public int Rtid { get; set; }
        public DateTime ReminderDate { get; set; }
    }

}