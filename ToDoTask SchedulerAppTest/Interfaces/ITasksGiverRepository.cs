using System.Collections.Generic;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface ITasksGivenRepository
    {
        ICollection<TasksGiven> GetTasks();
        ICollection<TasksGiven> GetTasksByUid(int uid);
        ICollection<Users> GetUsersByTid(int tid);
        TasksGiven GetTaskGivenByUidAndTid(int Tuid, int Ttid);
        bool TasksExistsByUid(int uid);
        bool UsersExistsByTid(int tid);
        bool TaskGivenExistsByUidAndTid(int uid, int tid);
        bool CreateTaskGiven(Users UidEntity, Tasks TidEntity);
        bool UpdateTaskGiven(TasksGiven taskgiven, int Tuid, int Ttid);
        bool DeleteTaskGiven(TasksGiven taskgiven);
        bool Save();
    }
}