using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Repository
{
    public class RemindersRepository : IRemindersRepository
    {
        private readonly ApplicationDbContext _context;
        public RemindersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Reminders> GetReminders()
        {
            return _context.Reminders.Include(r => r.Rtask).OrderBy(r => r.Rid).ToList();
        }

        public Reminders GetReminderById(int rid)
        {
            return _context.Reminders.Include(r => r.Rtask).Where(r => r.Rid == rid).FirstOrDefault();
        }

        public ICollection<Reminders> GetRemindersByDate(DateTime date)
        {
            return _context.Reminders.Include(r => r.Rtask).Where(r => r.ReminderDate == date).ToList();
        }

        public ICollection<Reminders> GetRemindersByUid(string uid)
        {
            return _context.Reminders.Include(r => r.Rtask).Where(r => r.Rauid == uid).ToList();
        }

        public ICollection<Reminders> GetRemindersByTid(int tid)
        {
            return _context.Reminders.Where(r => r.Rtid == tid).ToList();
        }

        public bool ReminderExistsById(int rid) {              return _context.Reminders.Any(r => r.Rid == rid);                }
        public bool RemindersExistsByUid(string uid) {         return _context.Reminders.Any(au => au.Rauid == uid);            }
        public bool RemindersExistsByDate(DateTime date) {     return _context.Reminders.Any(r => r.ReminderDate == date);      }
        public bool RemindersExistsByTid(int tid) {            return _context.Reminders.Any(r => r.Rtid == tid);               }
        public bool CreateReminder(Reminders reminder)
        {
            _context.Reminders.Add(reminder);
            return Save();
        }

        public bool UpdateReminder(Reminders reminder)
        {
            _context.Reminders.Update(reminder);
            return Save();
        }

        /*     
        public bool CreateReminder(Reminders reminder, Tasks RtidEntity, Users RuidEntity)
        {
            reminder.Rtid = RtidEntity;
            reminder.Ruid = RuidEntity;
            _context.Add(reminder);

            return Save();
        }
        *///CreateReminder OLD
        /*
        public bool UpdateReminder(Reminders reminder, Tasks RtidEntity, Users RuidEntity)
        {
            reminder.Rtid = RtidEntity;
            reminder.Ruid = RuidEntity;
            _context.Update(reminder);

            return Save();
        }
        *///UpdateReminder OLD

        public bool DeleteReminder(Reminders reminder)
        {
            _context.Remove(reminder);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
