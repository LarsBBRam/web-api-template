using Microsoft.AspNetCore.Mvc;
using web_api_template.Interfaces;

namespace web_api_template.Controllers;

[ApiController]
[Route("/[Controller]")]
public class TasksController(ITaskContext context, ILogger<TasksController> logger) : ControllerBase
{



}