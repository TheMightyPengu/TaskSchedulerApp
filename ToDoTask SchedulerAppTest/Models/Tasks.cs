using System.ComponentModel.DataAnnotations;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Models
{
    //public class Tasks
    //{
    //    [Key]
    //    public int Tid { get; set; }
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public DateTime Due { get; set; }
    //    public Admins? Taid { get; set; }
    //    public ICollection<TasksGiven> TasksGiven { get; set; }
    //}
    public class Tasks
    {
        [Key]
        public int Tid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public string Tauid { get; set; }
        public ApplicationUser Tau { get; set; }
        public ICollection<TasksGiven> TasksGiven { get; set; }
    }

}

