using System.Linq;
using ToDoTask_SchedulerAppTest.Data;
using ToDoTask_SchedulerAppTest.Interfaces;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        { 
            _context = context;
        }

        public Users GetUserById(int uid)
        {
            return _context.Users.Where(u => u.Uid == uid).FirstOrDefault();
        }

        public Users GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public Users GetUserByFullname(string fullName)
        {
            return _context.Users.Where(u => u.Fullname == fullName).FirstOrDefault();
        }

        public ICollection<Users> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Uid).ToList();
        }

        public bool UserExistsById(int uid)
        {
            return _context.Users.Any(u => u.Uid == uid);
        }
        public bool UserExistsByUsername(string userName)
        {
            return _context.Users.Any(u => u.Username == userName);
        }
        public bool UserExistsByFullname(string fullName)
        {
            return _context.Users.Any(u => u.Fullname == fullName);
        }
    }
}
