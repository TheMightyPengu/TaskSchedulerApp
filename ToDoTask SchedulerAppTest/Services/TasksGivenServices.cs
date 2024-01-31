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
        private readonly DataContext _context;
        private readonly ITasksGivenRepository _tasksgivenRepository;

        public TasksGivenServices(DataContext context, ITasksGivenRepository tasksgivenRepository)
        {
            _context = context;
            _tasksgivenRepository = tasksgivenRepository;
        }

        public (bool CanCreate, Users? UidEntity, Tasks? TidEntity, string? ErrorMessage) CheckCreateTaskGiven(int Uid, int Tid)
        {
            var UidEntity = _context.Users.Find(Uid);
            var TidEntity = _context.Tasks.Find(Tid);

            if (UidEntity == null)
                return (false, null, null, "User not found.");
            else if (TidEntity == null)
                return (false, null, null, "Task not found.");

            var userHasTaskAssigned = _context.TasksGiven.Any(tg => tg.User.Uid == Uid && tg.Task.Tid == Tid);

            if (!userHasTaskAssigned)
                return (false, null, null, "User does not have the specified task assigned.");

            return (true, UidEntity, TidEntity, null);
        }

        public (bool CanUpdate, string? ErrorMessage) CheckUpdateTaskGiven(int Tuid, int Ttid, TasksGivenUpdateDto UpdatedTaskGiven)
        {
            if (_context.Users.Find(Tuid) == null)
                return (false, "User not found.");
            else if (_context.Tasks.Find(Ttid) == null)
                return (false, "Task not found.");

            if (UpdatedTaskGiven == null)
                return (false, "Invalid task given");

            if (!_tasksgivenRepository.TaskGivenExistsByUidAndTid(Tuid, Ttid))
                return (false, "Task given not found");

            return (true, null);

        }


    }
}
