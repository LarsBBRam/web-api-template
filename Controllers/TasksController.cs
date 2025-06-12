using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using web_api_template.Interfaces;
using web_api_template.Models;

namespace web_api_template.Controllers;

[ApiController]
[Route("/[Controller]")]
public class TasksController(ITaskContext context, ILogger<TasksController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] QueryDto dto)
    {
        logger.LogInformation("Received Get request on standard route!");
        return Ok(context.GetAllTasks());
    }

    [HttpGet("complete")]
    public IActionResult GetComplete()
    {
        return Ok(context.GetCompleteTasks());
    }

    [HttpGet("pending")]
    public IActionResult GetPending()
    {
        return Ok(context.GetPendingTasks());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var task = context.GetTaskById(id);
        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPatch("complete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    public IActionResult Patch(int id)
    {
        var completedTask = context.CompleteTask(id);
        if (completedTask) return NoContent();
        else return NotFound();
    }

    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var deletedTask = context.DeleteTask(id);
        if (deletedTask) return NoContent();
        else return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] UserTaskDto dto)
    {
        return Ok(dto.InsertTask(context));
    }

}