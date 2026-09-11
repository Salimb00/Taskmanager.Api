namespace TaskManager.Api.Models;

public class TodoTask
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public string Priority { get; set; } = "Medium";

    public DateTime? DueDate { get; set; }
}