namespace TaskManager.Api.Services;

public static class TaskValidator
{
    public static bool IsValidPriority(string priority)
    {
        return priority is "Low" or "Medium" or "High";
    }
}