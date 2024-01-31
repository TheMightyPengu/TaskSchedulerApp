using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Principal;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Dto;
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
        public (bool CanCreate, Users? RuidEntity, Tasks? RtidEntity, string? ErrorMessage) CheckCreateUpdateReminder(int Ruid, int Rtid)
        {
            var RuidEntity = _context.Users.Find(Ruid);
            var RtidEntity = _context.Tasks.Find(Rtid);

            if (RuidEntity == null)
                return (false, null, null, "User not found.");
            else if (RtidEntity == null)
                return (false, null, null, "Task not found.");

            var userHasTaskAssigned = _context.TasksGiven.Any(tg => tg.User.Uid == Ruid && tg.Task.Tid == Rtid);
            if (!userHasTaskAssigned)
            {
                return (false, null, null, "User does not have the specified task assigned.");
            }

            return (true, RuidEntity, RtidEntity, null);
        }

        public ObjectResult CheckGetReminders(List<RemindersDto> reminders, ModelStateDictionary ModelState)
        {
            if (reminders == null || !reminders.Any())
            {
                return new ObjectResult("No reminders found.") {StatusCode = 404};
            }

            if (!ModelState.IsValid)
            {
                return new ObjectResult("Model state isnt valid") {StatusCode = 400};
            }

            return new ObjectResult(reminders) {StatusCode = 200};
        }


    }
}