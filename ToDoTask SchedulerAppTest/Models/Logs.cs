using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Models
{
    public class Logs
    {
        [Key]
        public int Lid { get; set; }
        public string Action { get; set; }
        public DateTime DateOfChange { get; set; }
        public Users Luid { get; set; }//dunno if its correct
        public Admins LaAid { get; set; }//dunno if its correct
    }
}