using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Models;


namespace ToDoTask_SchedulerAppTest.Services
{
    public class AlarmCheckServices : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AlarmCheckServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CheckDueReminders();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private void CheckDueReminders()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var dueReminders = dbContext.Reminders.Where(r => r.ReminderDate <= DateTime.Now && r.ReminderDate > DateTime.Now.AddMinutes(-1)).ToList();

                if (dueReminders.Any())
                {
                    foreach (var reminder in dueReminders)
                        System.Diagnostics.Debug.WriteLine($"Reminder due: {reminder.Rid}");
                }
            }
        }
    }
}

