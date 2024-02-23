using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Security.Principal;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Dto;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using ToDoTask_SchedulerAppTest.Repository;


namespace ToDoTask_SchedulerAppTest.Services
{

    public class TasksGivenServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ITasksGivenRepository _tasksgivenRepository;

        public TasksGivenServices(ApplicationDbContext context, ITasksGivenRepository tasksgivenRepository)
        {
            _context = context;
            _tasksgivenRepository = tasksgivenRepository;
        }

        public (bool canUpdate, string? errorMessage) ValidateTaskGivenEntities(string newTGauid, int newTGtid, TasksGivenUpdateDto? TaskGiven, bool isUpdating)
        {
            if (!_context.Users.Any(u => u.Id == newTGauid))
                return (false, "User not found.");
            else if (!_context.Tasks.Any(t => t.Tid == newTGtid))
                return (false, "Task not found.");

            if (isUpdating && !_tasksgivenRepository.TaskGivenExistsByUidAndTid(TaskGiven.TGauid, TaskGiven.TGtid))
                return (false, "TaskGiven not found");

            if (isUpdating && _tasksgivenRepository.TaskGivenExistsByUidAndTid(newTGauid, newTGtid))
                return (false, "TaskGiven already exists");

            return (true, null);
        }
        public void DeleteTask(int taskId)
        {
            var remindersToDelete = _context.Reminders.Where(r => r.Rtid == taskId);
            _context.Reminders.RemoveRange(remindersToDelete);

            var tasksGivenToDelete = _context.TasksGiven.Where(tg => tg.TGtid == taskId);
            _context.TasksGiven.RemoveRange(tasksGivenToDelete);

            var taskToDelete = _context.Tasks.FirstOrDefault(t => t.Tid == taskId);
            if (taskToDelete != null)
                _context.Tasks.Remove(taskToDelete);
            
            _context.SaveChanges();
        }


    }
}
