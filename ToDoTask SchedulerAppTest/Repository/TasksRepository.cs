using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Cryptography;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoTask_SchedulerAppTest.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Tasks> GetTasksByDue(DateTime date)
        {
            return _context.Tasks.OrderBy(t => t.Due == date).ToList();
        }

        public Tasks GetTaskById(int tid)
        {
            return _context.Tasks.Where(t => t.Tid == tid).FirstOrDefault();
        }

        //public ICollection<Tasks> GetTasksByUid(int uid)
        //{
        //    return _context.Set<TasksGiven>().Where(tg => tg.User.Uid == uid).Select(TasksGiven => TasksGiven.Task).ToList();
        //}
        public ICollection<Tasks> GetTasksByUid(string uid)
        {
            return _context.TasksGiven.Where(tg => tg.TGauid == uid).Select(tg => tg.TGtask).ToList();
        }

        public ICollection<Tasks> GetTasks()
        {
            return _context.Tasks.OrderBy(t => t.Tid).ToList();
        }

        public bool TasksExistsByDue(DateTime date)
        {
            return _context.Tasks.Any(t => t.Due == date);
        }
        public bool TaskExistsById(int tid)
        {
            return _context.Tasks.Any(t => t.Tid == tid);
        }

        //public bool TasksExistsByUid(int uid)
        //{
        //    return _context.Set<Users>().Any(u => u.Uid == uid);
        //}

        public bool TasksExistsByUid(string uid)
        {
            return _context.Users.Any(au => au.Id == uid);
        }

        public bool CreateTask(Tasks task)
        {
            _context.Add(task);
            return Save();
        }

        public bool UpdateTask(Tasks task)
        {
            _context.Update(task);
            return Save();
        }

        public bool DeleteTask(Tasks task)
        {
            _context.Remove(task);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
