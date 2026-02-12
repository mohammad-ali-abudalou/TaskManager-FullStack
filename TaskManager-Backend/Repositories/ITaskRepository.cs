using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(string userId); // Get all tasks for a specific user.
    Task AddTaskAsync(TaskItem task); // Add a new task to the database.
    Task DeleteTaskAsync(int id); // Mark a task as deleted by setting the IsDeleted flag to true instead of removing it from the database.
    Task UpdateTaskAsync(TaskItem task);  // Update an existing task's details, such as title, description, or completion status.
}
