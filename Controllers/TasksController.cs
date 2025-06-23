using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api_template.Interfaces;
using web_api_template.Models;

namespace web_api_template.Controllers;

[ApiController]
[Route("/[Controller]")]
public class TasksController(ITaskContext context, ILogger<TasksController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryDto dto)
    {
        logger.LogInformation("Received Get request on standard route!");
        return Ok(await dto.BuildQuery(context));
    }

    [HttpGet("complete")]
    public async Task<IActionResult> GetComplete()
    {
        return Ok(await context.GetCompleteTasks());
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        return Ok(await context.GetPendingTasks());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var task = await context.GetTaskById(id);
        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPatch("complete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    public async Task<IActionResult> Patch(int id)
    {
        var completedTask = await context.CompleteTask(id);
        if (completedTask) return NoContent();
        else return NotFound();
    }

    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTask = await context.DeleteTask(id);
        if (deletedTask) return NoContent();
        else return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] UserTaskDto dto)
    {
        return Ok(await dto.InsertTask(context));
    }

}