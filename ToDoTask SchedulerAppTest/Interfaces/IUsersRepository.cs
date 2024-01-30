using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<Users> GetUsers();
        Users GetUserById(int uid);
        Users GetUserByUsername(string username);
        Users GetUserByFullname(string fullName);
        ICollection<Users> GetUsersByTid(int tid);

        bool UserExistsById (int uid);
        bool UserExistsByUsername(string username);
        bool UserExistsByFullname(string fullName);
        bool UserExistsByTid(int tid);
        bool CreateUser(Users user);
        bool DeleteUser(Users user);
        bool Save();
    }
}
