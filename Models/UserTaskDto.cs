using System.Text.Json.Serialization;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class UserTaskDto
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("dueDate")]
    public required DateTime DueDate { get; set; }

    public async Task<UserTask> InsertTask(ITaskContext context)
    {
        return await context.AddTask(Title, Description, DueDate);
    }
}