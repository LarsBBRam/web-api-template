using web_api_template.Controllers;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class QueryDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public IQueryable<IUserTask> BuildQuery(TaskContext context)
    {
        var query = context.GetAllTasks().AsQueryable();
        if (!string.IsNullOrWhiteSpace(Title)) query = query.Where(task => task.Title.Contains(Title, StringComparison.InvariantCultureIgnoreCase));
        if (!string.IsNullOrWhiteSpace(Description)) query = query.Where(task => task.Description.Contains(Description, StringComparison.InvariantCultureIgnoreCase));
        if (StartDate.HasValue) query = query.Where(task => task.DueDate > StartDate);
        if (EndDate.HasValue) query = query.Where(task => task.DueDate < EndDate);
        return query;
    }
}