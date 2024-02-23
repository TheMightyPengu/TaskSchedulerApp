using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class TasksGivenDto
    {
        [Key]
        public string TGauid { get; set; }
        public int TGtid { get; set; }
        public UsersDto TGau { get; set; }
        public TasksDto TGtask { get; set; }
    }
}