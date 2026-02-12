namespace TaskManager.DTOs
{
    public class TaskResponseDto
    {
        // Gets or sets the Id.
        public int Id { get; set; }

        // Gets or sets the Title.
        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = string.Empty; // Task title with a maximum length of 100 characters.

        // Gets or sets the Description.
        public string Description { get; set; } = string.Empty; // Task description providing more details about the task.

        // Gets or sets a value indicating whether IsCompleted.
        public bool IsCompleted { get; set; }

        // Gets or sets a value indicating whether IsDeleted.
        public bool IsDeleted { get; set; } = false; // Indicates if the task is marked as deleted without actually removing it from the database.


        // Gets or sets the CreatedAt.
        public DateTime CreatedAt { get; set; }

        // Gets or sets the UserId.
        public string UserId { get; set; } = string.Empty; // To link the task to its owner, ensuring that tasks are associated with the correct user in a multi-user environment.
    }
}
