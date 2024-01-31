using Microsoft.EntityFrameworkCore;
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

        public ICollection<Reminders> GetRemindersByDate(DateTime date)
        {
            return _context.Reminders.Include(r => r.Rtid).Where(r => r.ReminderDate == date).ToList();
        }

        public Reminders GetReminderById(int rid)
        {
            return _context.Reminders.Include(r => r.Rtid).Where(r => r.Rid == rid).FirstOrDefault();
        }

        public ICollection<Reminders> GetRemindersByUid(int uid)
        {
            return _context.Reminders.Include(r => r.Rtid).Where(r => r.Ruid.Uid == uid).ToList();
        }

        public ICollection<Reminders> GetReminders()
        {
            return _context.Reminders.Include(r => r.Rtid).OrderBy(r => r.Rid).ToList();
        }

        public bool ReminderExistsById(int rid)
        {
            return _context.Reminders.Any(r => r.Rid == rid);
        }
        public bool RemindersExistsByUid(int uid)
        {
            return _context.Reminders.Any(r => r.Ruid.Uid == uid);
        }
        public bool RemindersExistsByDate(DateTime date)
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

        public bool DeleteReminder(Reminders reminder)
        {
            _context.Remove(reminder);
            return Save();
        }
        public bool UpdateReminder(Reminders reminder, Users RuidEntity, Tasks RtidEntity)
        {
            reminder.Rtid = RtidEntity;
            reminder.Ruid = RuidEntity;
            _context.Update(reminder);

            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
