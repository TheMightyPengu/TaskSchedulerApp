using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface IRemindersRepository
    {
        ICollection<Reminders> GetReminders();
        Reminders GetReminderById(int rid);
        ICollection<Reminders> GetRemindersByUid(string uid);
        ICollection<Reminders> GetRemindersByDate(DateTime date);
        bool ReminderExistsById(int rid);
        bool RemindersExistsByUid(string uid);
        bool RemindersExistsByDate(DateTime date);
        bool CreateReminder(Reminders reminder);
        bool UpdateReminder(Reminders reminder);
        bool DeleteReminder(Reminders reminder);
        bool Save();
    }
}
