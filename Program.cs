
using web_api_template.Interfaces;
using web_api_template.Models;

namespace web_api_template;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddLogging();
        builder.Services.AddSingleton<ITaskContext, TaskContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Swagger er ikke auto-inkludert i net9.0


        app.UseStaticFiles();

        app.MapFallbackToFile("index.html");

        app.UseHttpsRedirection();

        app.MapGet("/helloworld", () => "Hello, World");
        /*
            app.MapGet("/tasks", (ITaskContext context) => context.GetAllTasks());

            app.MapGet("/tasks/complete", (ITaskContext context) => context.GetCompleteTasks());

            app.MapGet("/tasks/pending", (ITaskContext context) => context.GetPendingTasks());

            app.MapGet("/tasks/{id}", (int id, ITaskContext context) => context.GetTaskById(id));

            app.MapPatch("/tasks/complete/{id}", (int id, ITaskContext context) => context.CompleteTask(id));

            app.MapDelete("/tasks/{id}", (int id, ITaskContext context) => context.DeleteTask(id));

            app.MapPost("/tasks", (string title, string description, DateTime dueDate, ITaskContext context) => context.AddTask(title, description, dueDate));

        */
        app.MapControllers();

        app.Run();
    }
}


