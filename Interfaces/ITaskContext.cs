using web_api_template.Models;

namespace web_api_template.Interfaces;

public interface ITaskContext
{
    int Count { get; }

    Task<List<UserTask>> GetAllTasks();

    Task<UserTask?> GetTaskById(int id);

    Task<List<UserTask>> GetPendingTasks();

    Task<List<UserTask>> GetCompleteTasks();

    Task<bool> CompleteTask(int id);

    Task<bool> DeleteTask(int id);

    Task<UserTask> AddTask(string title, string description, DateTime dueDate);
}