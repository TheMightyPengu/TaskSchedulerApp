using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class UsersDto
    {
        public string AUid { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
