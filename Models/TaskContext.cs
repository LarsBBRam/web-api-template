using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options), ITaskContext
{
    public DbSet<UserTask> Tasks { get; set; }
    public int Count => Tasks.Count();

    public async Task<UserTask> AddTask(string title, string description, DateTime dueDate)
    {
        var newTask = new UserTask(title, description, dueDate);
        await Tasks.AddAsync(newTask);
        await SaveChangesAsync();
        return newTask;
    }

    public async Task<bool> CompleteTask(int id)
    {
        var task = await Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) return false;
        task.IsCompleted = true;
        await SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await Tasks.FirstOrDefaultAsync(task => task.Id == id);
        if (task is null) return false;
        Tasks.Remove(task);
        await SaveChangesAsync();
        return true;
    }

    public async Task<List<UserTask>> GetAllTasks()
    {
        return await Tasks.ToListAsync();
    }

    public async Task<List<UserTask>> GetCompleteTasks()
    {
        return await Tasks.Where(task => task.IsCompleted).ToListAsync();
    }

    public async Task<List<UserTask>> GetPendingTasks()
    {
        return await Tasks.Where(task => !task.IsCompleted).ToListAsync();
    }

    public Task<UserTask?> GetTaskById(int id)
    {
        return Tasks.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id);
    }
}