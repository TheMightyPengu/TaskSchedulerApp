using System;
using System.Linq;
using ToDoTask_SchedulerAppTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoTask_SchedulerAppTest.Data;

namespace ToDoTask_SchedulerAppTest
{
    public class Seed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private ApplicationUser? _firstUser;
        private ApplicationUser? _secondUser;
        private Tasks? _firstTask;
        private Tasks? _secondTask;
        private readonly ILogger<Seed> _logger;
        public Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, ILogger<Seed> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedDataContextAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await InitializeUsersAndTasksAsync();
            await SeedTasksAsync();
            await SeedRemindersAsync();
            await SeedTasksGivenAsync();
        }

        private async Task InitializeUsersAndTasksAsync()
        {
                _firstUser = await _userManager.FindByEmailAsync("user1@example.com");
                _secondUser = await _userManager.FindByEmailAsync("user2@example.com");

                _firstTask = await _context.Tasks.OrderBy(t => t.Tid).FirstOrDefaultAsync();
                _secondTask = await _context.Tasks.OrderBy(t => t.Tid).Skip(1).FirstOrDefaultAsync();
        }

        private async Task SeedRolesAsync()
        {
            System.Diagnostics.Debug.WriteLine("started creating roles");
            if (!await _roleManager.Roles.AnyAsync())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "Admin" },
                    new IdentityRole { Name = "User" }
                };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
            System.Diagnostics.Debug.WriteLine("stoped creating roles");
        }

        private async Task SeedUsersAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                // Admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "IdkWhyThisIsHere",
                    Email = "admin1@example.com",
                    Fullname = "I am the first seeded admin"
                };
                if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(adminUser, "AdminPassword1");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                        _logger.LogInformation("Admin1 created");
                    }else {_logger.LogWarning("Admin1 not created. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));}
                }

                var secondadminUser = new ApplicationUser
                {
                    UserName = "maybeIshouldMakeItThePK",
                    Email = "admin2@example.com",
                    Fullname = "I am the second seeded admin"
                };
                if (await _userManager.FindByEmailAsync(secondadminUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(secondadminUser, "AdminPassword2");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(secondadminUser, "Admin");
                        _logger.LogInformation("Admin2 created");
                    }
                    else { _logger.LogWarning("Admin1 not created. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description))); }
                }

                // General user
                var generalUser = new ApplicationUser
                {
                    UserName = "MaybeItShouldDeleteIt",
                    Email = "user1@example.com",
                    Fullname = "I am the first seeded user"
                };
                if (await _userManager.FindByEmailAsync(generalUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(generalUser, "UserPassword1");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(generalUser, "User");
                        _logger.LogInformation("User1 created");
                    }
                    else { _logger.LogWarning("User1 not created. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description))); }

                }

                var secondgeneralUser = new ApplicationUser
                {
                    UserName = "GonnaLeaveItBe",
                    Email = "user2@example.com",
                    Fullname = "I am the second seeded user"
                };
                if (await _userManager.FindByEmailAsync(secondgeneralUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(secondgeneralUser, "UserPassword2");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(secondgeneralUser, "User");
                        _logger.LogInformation("User2 created");
                    }else { _logger.LogWarning("User2 not created. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description))); }
                }
            }
        }
        private async Task SeedTasksAsync()
        {
            if (!await _context.Tasks.AnyAsync())
            {
                var tasks = new List<Tasks>
                {
                    new Tasks
                    {
                        Title = "Task 1",
                        Description = "Description for Task 1",
                        Due = DateTime.Now
                    },
                    new Tasks
                    {
                        Title = "Task 2",
                        Description = "Description for Task 2",
                        Due = DateTime.Now.AddDays(1)
                    },
                    new Tasks
                    {
                        Title = "Task 3",
                        Description = "Description for Task 3",
                        Due = DateTime.Now.AddDays(2)
                    }
                };

                _context.Tasks.AddRange(tasks);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedRemindersAsync()
        {
            if (!await _context.Reminders.AnyAsync() && _firstUser != null && _secondUser != null && _firstTask != null && _secondTask != null)
            {
                var reminders = new List<Reminders>
                {
                    new Reminders
                    {
                        ReminderDate = DateTime.Now,
                        Rauid = _firstUser.Id,
                        Rtid = _firstTask.Tid
                    },
                    new Reminders
                    {
                        ReminderDate = DateTime.Now.AddDays(1),
                        Rauid = _firstUser.Id,
                        Rtid = _secondTask.Tid
                    },
                    new Reminders
                    {
                        ReminderDate = DateTime.Now.AddDays(10),
                        Rauid = _secondUser.Id,
                        Rtid = _secondTask.Tid
                    }
                };

                _context.Reminders.AddRange(reminders);
                await _context.SaveChangesAsync();
            }
        }


        private async Task SeedTasksGivenAsync()
        {
            if (!await _context.TasksGiven.AnyAsync() && _firstUser != null && _secondUser != null && _firstTask != null && _secondTask != null)
            {
                var tasksGiven = new List<TasksGiven>
                {
                    new TasksGiven { TGauid = _firstUser.Id, TGtid = _firstTask.Tid },
                    new TasksGiven { TGauid = _firstUser.Id, TGtid = _secondTask.Tid },
                    new TasksGiven { TGauid = _secondUser.Id, TGtid = _secondTask.Tid }
                };

                await _context.TasksGiven.AddRangeAsync(tasksGiven);
                await _context.SaveChangesAsync();
            }
        }



    }
}
