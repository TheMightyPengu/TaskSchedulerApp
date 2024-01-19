using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class Users
    {
        [Key]
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public ICollection<TasksGiven> TasksGivens { get; set; }
    }
}
