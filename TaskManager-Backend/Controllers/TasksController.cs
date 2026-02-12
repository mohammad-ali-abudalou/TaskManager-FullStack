using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        // This line retrieves the user ID from the claims of the authenticated user. It first tries to find the claim with the type ClaimTypes.NameIdentifier,
        // which is commonly used to store the unique identifier of the user (like a user ID). If that claim is not found,
        // it falls back to looking for a claim with the type ClaimTypes.Name, which typically contains the username.
        // This approach ensures that we can identify the user regardless of how their identity is represented in the claims.
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(ClaimTypes.Name)?.Value;

        var tasks = await repository.GetAllTasksAsync(userId!);
        return Ok(tasks);

    }

    [HttpPost]
    public async Task<ActionResult> Create(TaskItem task)
    {
        // Associate the task with the currently authenticated user.
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(ClaimTypes.Name)?.Value;
        task.UserId = userId!;

        // Ensure the server time is set correctly for accuracy. 
        // This is important for sorting and displaying tasks based on creation time.
        task.CreatedAt = DateTime.UtcNow;

        await repository.AddTaskAsync(task);

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await repository.DeleteTaskAsync(id);
        return NoContent();
    }

    // Update the task (e.g., change the state).
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        await repository.UpdateTaskAsync(task);
        return NoContent();
    }

}
