using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<Users> GetUsers();
        Users GetUserById(int uid);
        Users GetUserByUsername(string username);
        Users GetUserByFullname(string fullName);
        bool UserExistsById (int uid);
        bool UserExistsByUsername(string username);
        bool UserExistsByFullname(string fullName);
    }
}
