using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersDto
    {
        [Key]
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
