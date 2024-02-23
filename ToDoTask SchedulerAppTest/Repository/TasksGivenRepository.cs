using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoTask_SchedulerAppTest.Repository
{
    public class TasksGivenRepository : ITasksGivenRepository
    {
        private readonly ApplicationDbContext _context;
        public TasksGivenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<TasksGiven> GetTasksGiven()
        {
            return _context.TasksGiven.Include(tg => tg.TGau).Include(tg => tg.TGtask).OrderBy(tg => tg.TGauid).ToList();
        }

        public ICollection<TasksGiven> GetTasksGivenByUid(string uid)
        {
            return _context.TasksGiven.Include(tg => tg.TGau).Include(tg => tg.TGtask).Where(tg => tg.TGauid == uid).ToList();
        }

        public TasksGiven GetTaskGivenByUidAndTid(string uid, int tid)
        {
            return _context.TasksGiven.Include(tg => tg.TGau).Include(tg => tg.TGtask).FirstOrDefault(tg => tg.TGauid == uid && tg.TGtid == tid);
        }

        public ICollection<TasksGiven> GetTasksGivenByTid(int tid)
        {
            return _context.TasksGiven.Where(tg => tg.TGtid == tid).ToList();
        }

        //public ICollection<Users> GetUsersByTid(int tid)
        //{
        //    return _context.TasksGiven.Where(tg => tg.TGtid == tid).Select(tg => tg.TGauid).ToList();
        //}
        public ICollection<ApplicationUser> GetUsersByTid(int TGtid)
        {
            return _context.TasksGiven.Where(tg => tg.TGtid == TGtid).Select(tg => tg.TGau).ToList();
        }

        public bool TasksGivenExistsByUid(string uid)
        {
            return _context.TasksGiven.Any(tg => tg.TGauid == uid);
        }
        public bool UsersExistsByTid(int tid)
        {
            return _context.TasksGiven.Any(tg => tg.TGtid == tid);
        }
        public bool TaskGivenExistsByUidAndTid(string uid, int tid)
        {
            return _context.TasksGiven.Any(tg => tg.TGauid == uid && tg.TGtid == tid);
        }
        public bool TasksGivenExistsByTid(int tid)
        {
            return _context.TasksGiven.Any(tg => tg.TGtid == tid);
        }

        public bool CreateTaskGiven(string newTGauid, int newTGtid)
        {
            var taskGiven = new TasksGiven
            {
                TGauid = newTGauid,
                TGtid = newTGtid
            };

            _context.TasksGiven.Add(taskGiven);

            return Save();
        }

        public bool UpdateTaskGiven(TasksGiven taskGiven, string newApplicationUserId, int newTaskId)
        {
            var existingTaskGiven = _context.TasksGiven.Find(taskGiven.TGauid, taskGiven.TGtid);
            if (existingTaskGiven == null)
                return false;

            _context.TasksGiven.Remove(existingTaskGiven);

            var newTaskGiven = new TasksGiven
            {
                TGauid = newApplicationUserId,
                TGtid = newTaskId
            };

            _context.TasksGiven.Add(newTaskGiven);

            return Save();
        }

        /*
        public bool CreateTaskGiven(Users UidEntity, Tasks TidEntity)
        {
            var taskGiven = new TasksGiven
            {
                User = UidEntity,
                Task = TidEntity
            };
            _context.Add(taskGiven);

            return Save();
        }
        *///CreateTaskGiven OLD
        /*
        public bool UpdateTaskGiven(TasksGiven taskgiven, int Tuid, int Ttid)
        {
            var existingTaskGiven = _context.TasksGiven.FirstOrDefault(tg => tg.Tuid == Tuid && tg.TGtid == Ttid);

            if (existingTaskGiven == null)
                return false;

            //existingTaskGiven.Tuid = taskgiven.Tuid;
            //existingTaskGiven.Ttid = taskgiven.Ttid;

            _context.TasksGiven.Remove(existingTaskGiven);

            var newTaskGiven = new TasksGiven
            {
                Tuid = taskgiven.Tuid,
                Ttid = taskgiven.TGtid
                // User = taskgiven.User,
                // Task = taskgiven.Task
            };
            _context.TasksGiven.Add(newTaskGiven);

            return Save();
        }
        *///UpdateTaskGiven OLD

        public bool DeleteTaskGiven(TasksGiven taskgiven)
        {
            _context.Remove(taskgiven);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
