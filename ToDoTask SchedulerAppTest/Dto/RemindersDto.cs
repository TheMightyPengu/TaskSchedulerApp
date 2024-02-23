using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersDto
    {
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
        public TasksDto Rtask { get; set; }
    }
}
