using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class Tasks
    {
        [Key]
        public int Tid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public Admins Taid { get; set; }//hmmm??is that correct
        public ICollection<TasksGiven> TasksGivens { get; set; }

    }
}
