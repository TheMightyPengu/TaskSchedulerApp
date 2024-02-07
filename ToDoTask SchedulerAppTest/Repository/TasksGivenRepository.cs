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
        private readonly IdentityDbContext<ApplicationUser> _context;
        public TasksGivenRepository(IdentityDbContext<ApplicationUser> context)
        {
            _context = context;
        }

        public ICollection<TasksGiven> GetTasks()
        {
            return _context.Set<TasksGiven>().Include(tg => tg.User).Include(tg => tg.Task).OrderBy(tg => tg.Tuid).ToList();
        }

        public ICollection<TasksGiven> GetTasksByUid(int uid)
        {
            return _context.Set<TasksGiven>().Include(tg => tg.User).Include(tg => tg.Task).Where(tg => tg.Tuid == uid).ToList();
        }

        public TasksGiven GetTaskGivenByUidAndTid(int uid, int tid)
        {
            return _context.Set<TasksGiven>().Include(tg => tg.User).Include(tg => tg.Task).FirstOrDefault(tg => tg.Tuid == uid && tg.Ttid == tid);
        }

        public ICollection<Users> GetUsersByTid(int tid)
        {
            return _context.Set<TasksGiven>().Where(tg => tg.Ttid == tid).Select(tg => tg.User).ToList();
        }
        public bool TasksExistsByUid(int uid)
        {
            return _context.Set<TasksGiven>().Any(tg => tg.Tuid == uid);
        }
        public bool UsersExistsByTid(int tid)
        {
            return _context.Set<TasksGiven>().Any(tg => tg.Ttid == tid);
        }
        public bool TaskGivenExistsByUidAndTid(int uid, int tid)
        {
            return _context.Set<TasksGiven>().Any(tg => tg.Tuid == uid && tg.Ttid == tid);
        }

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
        public bool UpdateTaskGiven(TasksGiven taskgiven, int Tuid, int Ttid)
        {
            var existingTaskGiven = _context.Set<TasksGiven>().FirstOrDefault(tg => tg.Tuid == Tuid && tg.Ttid == Ttid);

            if (existingTaskGiven == null)
                return false;

            //existingTaskGiven.Tuid = taskgiven.Tuid;
            //existingTaskGiven.Ttid = taskgiven.Ttid;

            _context.Set<TasksGiven>().Remove(existingTaskGiven);

            var newTaskGiven = new TasksGiven
            {
                Tuid = taskgiven.Tuid,
                Ttid = taskgiven.Ttid
                // User = taskgiven.User,
                // Task = taskgiven.Task
            };
            _context.Set<TasksGiven>().Add(newTaskGiven);

            return Save();
        }
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
