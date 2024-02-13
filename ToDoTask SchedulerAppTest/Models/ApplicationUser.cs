using Microsoft.AspNetCore.Identity;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public virtual ICollection<TasksGiven> TasksGiven { get; set; }
    }
}
