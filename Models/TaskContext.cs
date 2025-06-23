using Microsoft.EntityFrameworkCore;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options), ITaskContext
{
    public DbSet<UserTask> Tasks { get; set; }
    public int Count => Tasks.Count();

    public IUserTask AddTask(string title, string description, DateTime dueDate)
    {
        var newTask = new UserTask(title, description, dueDate);
        Tasks.Add(newTask);
        SaveChanges();
        return newTask;
    }

    public bool CompleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        task.IsCompleted = true;
        SaveChanges();
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(task => task.Id == id);
        if (task is null) return false;
        Tasks.Remove(task);
        SaveChanges();
        return true;
    }

    public List<IUserTask> GetAllTasks()
    {
        return [.. Tasks.AsNoTracking()];
    }

    public List<IUserTask> GetCompleteTasks()
    {
        return [.. Tasks.Where(task => task.IsCompleted).AsNoTracking()];
    }

    public List<IUserTask> GetPendingTasks()
    {
        return [.. Tasks.Where(task => !task.IsCompleted).AsNoTracking()];
    }

    public IUserTask? GetTaskById(int id)
    {
        return Tasks.AsNoTracking().FirstOrDefault(task => task.Id == id);
    }
}