using Microsoft.EntityFrameworkCore;
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

        public bool AssignTask(Users UidEntity, Tasks TidEntity)
        {
            //TaskGiven.Tuid = UidEntity;
            //TaskGiven.Ttid = TidEntity;
            //_context.Add(TaskGiven);

            var user = _context.Users.Find(UidEntity.Uid);
            var task = _context.Tasks.Find(TidEntity.Tid);
            var taskGiven = new TasksGiven
            {
                User = user,
                Task = task
            };
            _context.Add(taskGiven);

            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
