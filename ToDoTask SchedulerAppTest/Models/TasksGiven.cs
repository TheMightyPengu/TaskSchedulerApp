using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class TasksGiven
    {
        [Key]
        public int Tuid { get; set; }
        public int Ttid { get; set; }
        public Users User { get; set; }
        public Tasks Task { get; set; }
    }
}
