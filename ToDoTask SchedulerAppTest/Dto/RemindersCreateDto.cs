﻿using System.ComponentModel.DataAnnotations;

namespace ToDoTask_SchedulerAppTest.Dto
{
    public class RemindersCreateDto
    {
        [Key]
        public int Rid { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}