
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
        builder.Services.AddSingleton<ITaskContext, TaskContext>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        app.MapFallbackToFile("index.html");

        app.UseHttpsRedirection();

        app.MapGet("/helloworld", () => "Hello, World");

        app.MapGet("/tasks", (ITaskContext context) => context.GetAllTasks());

        app.MapGet("/tasks/complete", (ITaskContext context) => context.GetCompleteTasks());

        app.MapGet("/tasks/pending", (ITaskContext context) => context.GetPendingTasks());

        app.Run();
    }
}



ER PÅ MINUTT 24!