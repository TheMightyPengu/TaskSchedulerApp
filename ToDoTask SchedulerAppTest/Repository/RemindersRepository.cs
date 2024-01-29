using System;
using System.Security.Cryptography;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Repository
{
    public class RemindersRepository : IRemindersRepository
    {
        private readonly DataContext _context;
        public RemindersRepository(DataContext context)
        {
            _context = context;
        }

        public Reminders GetReminderByDate(DateTime date)
        {
            return _context.Reminders.Where(r => r.ReminderDate == date).FirstOrDefault();
        }

        public Reminders GetReminderById(int rid)
        {
            return _context.Reminders.Where(r => r.Rid == rid).FirstOrDefault();
        }

        public Reminders GetReminderByUid(int uid)
        {
            return _context.Reminders.Where(r => r.Ruid.Uid == uid).FirstOrDefault();//might be wrong
        }

        public ICollection<Reminders> GetReminders()
        {
            return _context.Reminders.OrderBy(r => r.Rid).ToList();
        }

        public bool ReminderExistsById(int rid)
        {
            return _context.Reminders.Any(r => r.Rid == rid);
        }
        public bool ReminderExistsByUid(int uid)
        {
            return _context.Reminders.Any(r => r.Ruid.Uid == uid);
        }
        public bool ReminderExistsByDate(DateTime date)
        {
            return _context.Reminders.Any(r => r.ReminderDate == date);

        }
        public bool CreateReminder(Reminders reminder, Users RuidEntity, Tasks RtidEntity)
        {
            reminder.Rtid = RtidEntity;
            reminder.Ruid = RuidEntity;
            _context.Add(reminder);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
