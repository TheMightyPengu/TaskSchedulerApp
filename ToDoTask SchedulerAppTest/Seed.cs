using System;
using System.Collections.Generic;
using System.Linq;
using ToDoTask_SchedulerAppTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoTask_SchedulerAppTest
{
    public class Seed
    {
        private readonly IdentityDbContext<ApplicationUser> identityContext;

        public Seed(IdentityDbContext<ApplicationUser> context)
        {
            identityContext = context;
        }

        public void SeedDataContext()
        {
            SeedNewData();
            SeedExistingData();
        }

        public void SeedNewData()
        {
            // Modify your seed data creation logic to use 'identityContext' instead of 'dataContext'
            // Example: identityContext.TasksGiven.AddRange(...)
        }

        public void SeedExistingData()
        {
            // Modify your existing data seeding logic if necessary
        }

        public void UpdateAdminById(int adminId, string newUsername, string newPassword, string newFullname, string newEmail)
        {
            var adminToUpdate = identityContext.Set<Admins>().Find(adminId);
            if (adminToUpdate != null)
            {
                adminToUpdate.Username = newUsername;
                adminToUpdate.Password = newPassword;
                adminToUpdate.Fullname = newFullname;
                adminToUpdate.Email = newEmail;
            }
        }

        private void UpdateUserById(int userId, string newUsername, string newPassword, string newFullname, string newEmail)
        {
            var userToUpdate = identityContext.Set<Users>().Find(userId);
            if (userToUpdate != null)
            {
                userToUpdate.Username = newUsername;
                userToUpdate.Password = newPassword;
                userToUpdate.Fullname = newFullname;
                userToUpdate.Email = newEmail;
            }
        }
    }
}
