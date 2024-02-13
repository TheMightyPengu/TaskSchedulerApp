using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public RemindersServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public (bool canCreate, string? ErrorMessage) ValidateReminderEntities(string Ruid, int Rtid)
        {
            if (_context.Users.Find(Ruid) == null)
                return (false, "User not found.");
            else if (_context.Tasks.Find(Rtid) == null)
                return (false, "Task not found.");

            var userHasTaskAssigned = _context.TasksGiven.Any(tg => tg.TGauid == Ruid && tg.TGtid == Rtid);

            if (!userHasTaskAssigned)
                return (false, "User does not have the specified task assigned.");
            
            return (true, null);
        }

        public ActionResult ValidateGetReminders(List<RemindersDto> reminders, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(new { Error = "Model state isn't valid." });

            if (reminders == null || !reminders.Any())
                return new NotFoundObjectResult(new { Message = "No reminders found." });

            return new OkObjectResult(reminders);
        }

    }
}