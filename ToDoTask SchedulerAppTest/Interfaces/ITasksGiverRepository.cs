using System.Collections.Generic;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface ITasksGivenRepository
    {
        ICollection<TasksGiven> GetTasks();
        ICollection<TasksGiven> GetTasksByUid(int uid);
        ICollection<Users> GetUsersByTid(int tid);
        bool TasksExistsByUid(int uid);
        bool UsersExistsByTid(int tid);
        bool AssignTask(Users UidEntity, Tasks TidEntity);
        bool Save();
    }
}