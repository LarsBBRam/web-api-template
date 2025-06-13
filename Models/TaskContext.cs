using Microsoft.EntityFrameworkCore;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options), ITaskContext
{
    public DbSet<UserTask> Tasks { get; set; }
    private int _nextId;
    public int Count => Tasks.Count();

    public IUserTask AddTask(string title, string description, DateTime dueDate)
    {
        var newTask = new UserTask(++_nextId, title, description, dueDate);
        Tasks.Add(newTask);
        return newTask;
    }

    public bool CompleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        task.MarkAsCompleted();
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        Tasks.Remove(task);
        return true;
    }

    public List<IUserTask> GetAllTasks()
    {
        return [.. Tasks];
    }

    public List<IUserTask> GetCompleteTasks()
    {
        return [.. Tasks.Where(task => task.IsCompleted)];
    }

    public List<IUserTask> GetPendingTasks()
    {
        return [.. Tasks.Where(task => !task.IsCompleted)];
    }

    public UserTask? GetTaskById(int id)
    {
        return Tasks.FirstOrDefault(task => task.Id == id);
    }
}