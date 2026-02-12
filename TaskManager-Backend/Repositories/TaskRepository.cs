using Microsoft.EntityFrameworkCore;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{

    public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(string userId)
    {
        return await context.Tasks
                            .Where(t => t.UserId == userId) // It will only fetch tasks specific to this user.
                            .OrderByDescending(t => t.CreatedAt) // Sort by creation date in descending order.
                            .Select(t => new TaskResponseDto // Mapping process.
                            {
                                Id = t.Id,
                                Title = t.Title,
                                Description = t.Description,
                                IsCompleted = t.IsCompleted,
                                CreatedAt = t.CreatedAt,
                                UserId = t.UserId,
                                IsDeleted = t.IsDeleted
                            })
                            .ToListAsync();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        await context.Tasks.AddAsync(task); // Add the new task to the database context.
        await context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task != null)
        {
            task.IsDeleted = true; // Mark the task as deleted instead of removing it from the database.
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        context.Entry(task).State = EntityState.Modified; // Update the existing task's details. This will track changes to the task and update it in the database when SaveChangesAsync is called.
        await context.SaveChangesAsync();
    }
}
