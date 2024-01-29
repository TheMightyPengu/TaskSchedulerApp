using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Services
{
    public class RemindersServices
    {
        private readonly DataContext _context;

        public RemindersServices(DataContext context)
        {
            _context = context;
        }
        public (bool CanCreate, Users? RuidEntity, Tasks? RtidEntity, string? ErrorMessage) CheckCreateReminder(int Rtid, int Ruid)
        {
            var RuidEntity = _context.Users.Find(Ruid);
            var RtidEntity = _context.Tasks.Find(Rtid);

            if (RuidEntity == null || RtidEntity == null)
            {
                return (false, null, null, "User or task not found.");
            }

            var userHasTaskAssigned = _context.TasksGiven.Any(tg => tg.User.Uid == Ruid && tg.Task.Tid == Rtid);
            if (!userHasTaskAssigned)
            {
                return (false, null, null, "User does not have the specified task assigned.");
            }

            return (true, RuidEntity, RtidEntity, null);
        }
    }
}