using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface IRemindersRepository
    {
        ICollection<Reminders> GetReminders();
        Reminders GetReminderById(int rid);
        ICollection<Reminders> GetRemindersByUid(int uid);
        ICollection<Reminders> GetRemindersByDate(DateTime date);
        bool ReminderExistsById(int rid);
        bool RemindersExistsByUid(int uid);
        bool RemindersExistsByDate(DateTime date);
        bool CreateReminder(Reminders reminder, Users RuidEntity, Tasks RtidEntity);
        bool UpdateReminder(Reminders reminder, Users RuidEntity, Tasks RtidEntity);
        bool DeleteReminder(Reminders reminder);
        bool Save();
    }
}
