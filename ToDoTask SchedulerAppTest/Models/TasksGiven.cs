using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    //public class TasksGiven
    //{
    //    [Key]
    //    public int Tuid { get; set; }
    //    public int Ttid { get; set; }
    //    public Users User { get; set; }
    //    public Tasks Task { get; set; }
    //}
    public class TasksGiven
    {
        [Key]
        public string TGauid { get; set; }
        public int TGtid { get; set; }
        public virtual ApplicationUser TGau { get; set; }
        public virtual Tasks TGtask { get; set; }
    }

}
