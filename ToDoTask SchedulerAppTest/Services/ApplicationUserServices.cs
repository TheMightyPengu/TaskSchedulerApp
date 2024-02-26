
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;

namespace ToDoTask_SchedulerAppTest.Services
{
    public class ApplicationUserServices
    {

        private readonly IRemindersRepository _remindersRepository;
        private readonly ITasksGivenRepository _tasksGivenRepository;
        private readonly ApplicationDbContext _context;

        public ApplicationUserServices(IRemindersRepository remindersRepository, ITasksGivenRepository tasksGivenRepository, ApplicationDbContext context)
        {
            _remindersRepository = remindersRepository;
            _tasksGivenRepository = tasksGivenRepository;
            _context = context;
        }

        public async Task DeleteReminderAndTaskGiven(string uid)
        {
            try { 
                var reminders = _remindersRepository.GetRemindersByUid(uid);
                if (reminders.Any())
                    _context.Reminders.RemoveRange(reminders);

                var tasksGiven = _tasksGivenRepository.GetTasksGivenByUid(uid);
                if (tasksGiven.Any())
                    _context.TasksGiven.RemoveRange(tasksGiven);

                await _context.SaveChangesAsync();

            }
            catch (Exception ) {
            }
        }


    }
}
