using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class TasksGivenDto
    {
        [Key]
        public int Tuid { get; set; }
        public int Ttid { get; set; }
        public UsersDto User { get; set; }
        public TasksDto Task { get; set; }
    }
}
