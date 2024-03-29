﻿//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using System.Linq;
//using System.Security.Cryptography;
//using ToDoTask_SchedulerAppTest.Data;
//using ToDoTask_SchedulerAppTest.Interfaces;
//using ToDoTask_SchedulerAppTest.Models;
//using Microsoft.EntityFrameworkCore;

//namespace ToDoTask_SchedulerAppTest.Repository
//{
//    public class UsersRepository : IUsersRepository
//    {
//        private readonly IdentityDbContext<ApplicationUser> _context;
//        public UsersRepository(IdentityDbContext<ApplicationUser> context)
//        { 
//            _context = context;
//        }

//        public Users GetUserById(int uid)
//        {
//            return _context.Set<Users>().Where(u => u.Uid == uid).FirstOrDefault();
//        }

//        public Users GetUserByUsername(string UserName)
//        {
//            return _context.Set<Users>().Where(u => u.UserName == UserName).FirstOrDefault();
//        }

//        public Users GetUserByFullname(string FullName)
//        {
//            return _context.Set<Users>().Where(u => u.FullName == FullName).FirstOrDefault();
//        }

//        public ICollection<Users> GetUsers()
//        {
//            return _context.Set<Users>().OrderBy(u => u.Uid).ToList();
//        }
//        public ICollection<Users> GetUsersByTid(int tid)
//        {
//            return _context.Set<TasksGiven>().Where(tg => tg.Task.Tid == tid).Select(TasksGiven => TasksGiven.User).ToList();
//        }

//        public bool UserExistsById(int uid)
//        {
//            return _context.Set<Users>().Any(u => u.Uid == uid);
//        }
//        public bool UserExistsByUsername(string UserName)
//        {
//            return _context.Set<Users>().Any(u => u.UserName == UserName);
//        }
//        public bool UserExistsByFullname(string FullName)
//        {
//            return _context.Set<Users>().Any(u => u.FullName == FullName);
//        }
//        public bool UserExistsByTid(int tid)
//        {
//            return _context.Set<Tasks>().Any(t => t.Tid == tid);
//        }

//        public bool CreateUser(Users user)
//        {
//            _context.Add(user);
//            return Save();
//        }
//        public bool UpdateUser(Users user)
//        {
//            _context.Update(user);
//            return Save();
//        }

//        public bool DeleteUser(Users user)
//        {
//            _context.Remove(user);
//            return Save();
//        }

//        public bool Save()
//        {
//            var saved = _context.SaveChanges();
//            return saved > 0 ? true : false;
//        }

//    }
//}
