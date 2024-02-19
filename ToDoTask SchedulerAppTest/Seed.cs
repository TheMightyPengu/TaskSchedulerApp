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

        public Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedDataContextAsync()
        {
            await InitializeUsersAndTasksAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedTasksAsync();
            await SeedRemindersAsync();
            await SeedTasksGivenAsync();
        }

        private async Task InitializeUsersAndTasksAsync()
        {
                _firstUser = await _userManager.FindByEmailAsync("user@example.com");
                _secondUser = await _userManager.FindByEmailAsync("seconduser@example.com");

                _firstTask = await _context.Tasks.OrderBy(t => t.Tid).FirstOrDefaultAsync();
                _secondTask = await _context.Tasks.OrderBy(t => t.Tid).Skip(1).FirstOrDefaultAsync();
        }

        private async Task SeedRolesAsync()
        {
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
        }

        private async Task SeedUsersAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                // Admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "idk why i have this",
                    Email = "admin1@example.com",
                    Fullname = "I am the first seeded admin"
                };
                if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
                {
                    await _userManager.CreateAsync(adminUser, "AdminPassword1");
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }

                var secondadminUser = new ApplicationUser
                {
                    UserName = "maybe i should make it the primary key",
                    Email = "admin2@example.com",
                    Fullname = "I am the second seeded admin"
                };
                if (await _userManager.FindByEmailAsync(secondadminUser.Email) == null)
                {
                    await _userManager.CreateAsync(secondadminUser, "AdminPassword2");
                    await _userManager.AddToRoleAsync(secondadminUser, "Admin");
                }

                // General user
                var generalUser = new ApplicationUser
                {
                    UserName = "maybe i should delete it",
                    Email = "user1@example.com",
                    Fullname = "I am the first seeded user"
                };
                if (await _userManager.FindByEmailAsync(generalUser.Email) == null)
                {
                    await _userManager.CreateAsync(generalUser, "UserPassword1");
                    await _userManager.AddToRoleAsync(generalUser, "User");
                }

                var secondgeneralUser = new ApplicationUser
                {
                    UserName = "i will just leave it be",
                    Email = "user2@example.com",
                    Fullname = "I am the second seeded user"
                };
                if (await _userManager.FindByEmailAsync(secondgeneralUser.Email) == null)
                {
                    await _userManager.CreateAsync(secondgeneralUser, "UserPassword2");
                    await _userManager.AddToRoleAsync(secondgeneralUser, "User");
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
