using TaskManager.Api.Models;

namespace TaskManager.Api.Services;

public static class TaskValidator
{
    public static bool IsValidPriority(string priority)
    {
        return priority is "Low" or "Medium" or "High";
    }

    public static string? ValidateTask(TodoTask task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return "Title is required.";
        }

        if (task.Title.Length > 100)
        {
            return "Title cannot be longer than 100 characters.";
        }

        if (task.Description.Length > 500)
        {
            return "Description cannot be longer than 500 characters.";
        }

        if (!IsValidPriority(task.Priority))
        {
            return "Priority must be Low, Medium, or High.";
        }

        return null;
    }
}