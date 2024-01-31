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
        private readonly DataContext _context;
        public TasksGivenRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<TasksGiven> GetTasks()
        {
            return _context.TasksGiven.Include(tg => tg.User).Include(tg => tg.Task).OrderBy(tg => tg.Tuid).ToList();
        }

        public ICollection<TasksGiven> GetTasksByUid(int uid)
        {
            return _context.TasksGiven.Include(tg => tg.User).Include(tg => tg.Task).Where(tg => tg.Tuid == uid).ToList();
        }

        public TasksGiven GetTaskGivenByUidAndTid(int uid, int tid)
        {
            return _context.TasksGiven.Include(tg => tg.User).Include(tg => tg.Task).FirstOrDefault(tg => tg.Tuid == uid && tg.Ttid == tid);
        }

        public ICollection<Users> GetUsersByTid(int tid)
        {
            return _context.TasksGiven.Where(tg => tg.Ttid == tid).Select(tg => tg.User).ToList();
        }
        public bool TasksExistsByUid(int uid)
        {
            return _context.TasksGiven.Any(tg => tg.Tuid == uid);
        }
        public bool UsersExistsByTid(int tid)
        {
            return _context.TasksGiven.Any(tg => tg.Ttid == tid);
        }
        public bool TaskGivenExistsByUidAndTid(int uid, int tid)
        {
            return _context.TasksGiven.Any(tg => tg.Tuid == uid && tg.Ttid == tid);
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
            var existingTaskGiven = _context.TasksGiven.FirstOrDefault(tg => tg.Tuid == Tuid && tg.Ttid == Ttid);

            if (existingTaskGiven == null)
                return false;

            existingTaskGiven.Tuid = taskgiven.Tuid;
            existingTaskGiven.Ttid = taskgiven.Ttid;

            //_context.Entry(existingTaskGiven).State = EntityState.Detached;
            //var newTaskGiven = new TasksGiven
            //{
            //    Tuid = taskgiven.Tuid,
            //    Ttid = taskgiven.Ttid,
            //    // If User and Task properties need to be updated, update them as well
            //    // User = taskgiven.User,
            //    // Task = taskgiven.Task
            //};
            //_context.Attach(newTaskGiven);

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
