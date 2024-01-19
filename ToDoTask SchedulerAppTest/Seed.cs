using System.Diagnostics.Metrics;
using System.Reflection;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context){ 
            dataContext = context; 
        }
        public void SeedDataContext()
        {
            SeedNewData();
            SeedExistingData();
        }

        public void SeedNewData() {
            if (!dataContext.TasksGiven.Any())
            {
                var TasksGivenList = new List<TasksGiven>()
                {
                    new TasksGiven()
                    {
                        User = new Users()
                        {
                            Username = "BestUsername01",
                            Password = "BestPassword01",
                            Fullname = "TestSubject01",
                            Email = "01@gmail.com",
                        },
                        Task = new Tasks()
                        {
                            Title = "besttask01",
                            Description = "this is the best task ever created",
                            Due = new DateTime(1111,1,1),
                            Taid = new Admins()
                            {
                                Username = "BestAdminUsername01",
                                Password = "BestAdminPassword01",
                                Fullname = "TestAdminSubject01",
                                Email = "01Admin@gmail.com",
                            },
                        }
                    },
                    new TasksGiven()
                    {
                        User = new Users()
                        {
                            Username = "BestUserName02",
                            Password = "BestPassword02",
                            Fullname = "TestSubject02",
                            Email = "02@gmail.com",
                        },
                        Task = new Tasks()
                        {
                            Title = "besttask02",
                            Description = "this is the SECOND best task ever created",
                            Due = new DateTime(2222,2,2),
                            Taid = new Admins()
                            {
                                Username = "BestAdminUsername02",
                                Password = "BestAdminPassword02",
                                Fullname = "TestAdminSubject02",
                                Email = "02Admin@gmail.com",
                            },
                        }
                    }
                };
                dataContext.TasksGiven.AddRange(TasksGivenList);
                dataContext.SaveChanges();
            }

            if (!dataContext.Reminders.Any())
            {
                Users user1 = dataContext.Users.FirstOrDefault(u => u.Uid == 1);
                Users user2 = dataContext.Users.FirstOrDefault(u => u.Uid == 2);
                Tasks task1 = dataContext.Tasks.FirstOrDefault(t => t.Tid == 1);
                Tasks task2 = dataContext.Tasks.FirstOrDefault(t => t.Tid == 2);

                var RemindersList = new List<Reminders>()
                {
                    new Reminders()
                    {
                        ReminderDate = new DateTime(2024,01,25).AddHours(8).AddMinutes(30),
                        Ruid = user1,
                        Rtid = task2

                    },
                    new Reminders()
                    {
                        ReminderDate = new DateTime(2024,02,28).AddHours(10).AddMinutes(20),
                        Ruid = user2,
                        Rtid = task1

                    },
                };
                dataContext.Reminders.AddRange(RemindersList);
                dataContext.SaveChanges();

            }
        }

        public void SeedExistingData()
        {
            UpdateAdminById(1, "BestAdminUsername01", "BestAdminPassword01", "TestAdminSubject01", "01Admin@gmail.com");
            UpdateAdminById(2, "BestAdminUsername02", "BestAdminPassword02", "TestAdminSubject02", "02Admin@gmail.com");

            UpdateUserById(1, "BestUsername01", "BestPassword01", "TestSubject01", "01@gmail.com");
            UpdateUserById(2, "BestUsername02", "BestPassword02", "TestSubject02", "02@gmail.com");

            dataContext.SaveChanges();
        }

        public void UpdateAdminById(int adminId, string newUsername, string newPasword, string newFullname, string newEmail)
        {
            var adminToUpdate = dataContext.Admins.Find(adminId);
            if (adminToUpdate != null)
            {
                adminToUpdate.Username = newUsername;
                adminToUpdate.Password = newPasword;
                adminToUpdate.Fullname = newFullname;
                adminToUpdate.Email = newEmail;
            }
        }
        private void UpdateUserById(int userId, string newUsername, string newPasword, string newFullname, string newEmail)
        {
            var userToUpdate = dataContext.Users.Find(userId);
            if (userToUpdate != null)
            {
                userToUpdate.Username = newUsername;
                userToUpdate.Password = newPasword;
                userToUpdate.Fullname = newFullname;
                userToUpdate.Email = newEmail;
            }
        }
    }
}