using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface IRemindersRepository
    {
        ICollection<Reminders> GetReminders();
        Reminders GetReminderById(int rid);
        Reminders GetReminderByUid(int uid);
        Reminders GetReminderByDate(DateTime date);
        bool ReminderExistsById(int rid);
        bool ReminderExistsByUid(int uid);
        bool ReminderExistsByDate(DateTime date);
    }
}
