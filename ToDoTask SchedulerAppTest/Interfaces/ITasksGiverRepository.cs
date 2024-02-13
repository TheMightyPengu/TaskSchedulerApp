using System.Collections.Generic;
using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface ITasksGivenRepository
    {
        ICollection<TasksGiven> GetTasksGiven();
        ICollection<TasksGiven> GetTasksGivenByUid(string uid);
        ICollection<ApplicationUser> GetUsersByTid(int tid);
        TasksGiven GetTaskGivenByUidAndTid(string Tuid, int Ttid);
        bool TasksGivenExistsByUid(string uid);
        bool UsersExistsByTid(int tid);
        bool TaskGivenExistsByUidAndTid(string uid, int tid);
        bool CreateTaskGiven(string TGauid, int TGtid);
        bool UpdateTaskGiven(TasksGiven taskGiven, string newApplicationUserId, int newTaskId);
        //bool CreateTaskGiven(Users UidEntity, Tasks TidEntity);
        //bool UpdateTaskGiven(TasksGiven taskgiven, int Tuid, int Ttid);
        bool DeleteTaskGiven(TasksGiven taskgiven);
        bool Save();
    }
}