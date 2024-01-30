using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Security.Principal;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Services
{

    public class TasksGivenServices
    {
        private readonly DataContext _context;

        public TasksGivenServices(DataContext context)
        {
            _context = context;
        }

        public (bool CanCreate, Users? UidEntity, Tasks? TidEntity, string? ErrorMessage) CheckAssignTask(int Uid, int Tid)
        {
            var UidEntity = _context.Users.Find(Uid);
            var TidEntity = _context.Tasks.Find(Tid);

            if (UidEntity == null)
                return (false, null, null, "User not found.");
            else if (TidEntity == null)
                return (false, null, null, "Task not found.");


            return (true, UidEntity, TidEntity, null);
        }

    }
}
