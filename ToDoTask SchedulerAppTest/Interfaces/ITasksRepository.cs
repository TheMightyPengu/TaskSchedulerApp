using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface ITasksRepository
    {
        ICollection<Tasks> GetTasks();
        Tasks GetTaskById(int tid);
        ICollection<Tasks> GetTasksByUid(int uid);
        Tasks GetTaskByDue(DateTime date);
        bool TaskExistsById(int tid);
        bool TaskExistsByUid(int uid);
        bool TaskExistsByDue(DateTime date);
        bool CreateTask(Tasks task);
        bool Save();
    }
}
