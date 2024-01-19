using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class UsersDto
    {
        [Key]
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
