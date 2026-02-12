namespace TaskManager.Models
{
    public class TaskItem
    {
        // Gets or sets the Id.
        public int Id { get; set; }

        // Gets or sets the Title.
        public string Title { get; set; } = string.Empty;

        // Gets or sets the Description.
        public string Description { get; set; } = string.Empty;

        // Gets or sets a value indicating whether IsCompleted.
        public bool IsCompleted { get; set; } = false; // Task completion status.

        // Gets or sets a value indicating whether IsDeleted.
        public bool IsDeleted { get; set; } = false; // Indicates if the task is marked as deleted without actually removing it from the database.

        // Gets or sets the CreatedAt.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the task was created.

        // Gets or sets the UserId.
        public string UserId { get; set; } = string.Empty; // To link the task to its owner.
    }
}
