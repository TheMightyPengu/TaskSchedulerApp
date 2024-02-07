using Microsoft.AspNetCore.Identity;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
