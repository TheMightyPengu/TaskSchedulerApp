using ToDoTask_SchedulerAppTest.Models;

namespace ToDoTask_SchedulerAppTest.Interfaces
{
    public interface ITasksRepository
    {
        ICollection<Tasks> GetTasks();
        Tasks GetTaskById(int tid);
        ICollection<Tasks> GetTasksByUid(int uid);
        ICollection<Tasks> GetTasksByDue(DateTime date);
        bool TaskExistsById(int tid);
        bool TasksExistsByUid(int uid);
        bool TasksExistsByDue(DateTime date);
        bool CreateTask(Tasks task);
        bool UpdateTask(Tasks task);
        bool DeleteTask(Tasks task);
        bool Save();
    }
}
