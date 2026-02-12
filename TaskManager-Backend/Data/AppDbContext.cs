using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Gets or sets the Tasks.
    public DbSet<TaskItem> Tasks { get; set; }

    // The OnModelCreating.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<TaskItem>().HasQueryFilter(t => !t.IsDeleted); // This ensures that any query on Tasks will automatically exclude those marked as deleted.
    }
}