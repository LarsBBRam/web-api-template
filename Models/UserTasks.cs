using System.ComponentModel.DataAnnotations;
using web_api_template.Interfaces;

namespace web_api_template.Models;

public class UserTask(string title, string description, DateTime dueDate) : IUserTask
{
    [Key]
    public int Id { get; init; }

    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; } = dueDate;

}