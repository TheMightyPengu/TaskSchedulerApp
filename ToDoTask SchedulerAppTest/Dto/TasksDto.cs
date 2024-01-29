using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class TasksDto
    {
        [Key]
        public int Tid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public Admins? Taid { get; set; }
    }
}
