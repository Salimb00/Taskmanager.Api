using TaskManager.Api.Services;
using TaskManager.Api.Models;

namespace TaskManager.Api.Tests;

public class TaskValidatorTests
{
    [Fact]
    public void ValidateTask_WithEmptyTitle_ReturnsError()
    {
        var task = new TodoTask
        {
            Title = "",
            Description = "Test description",
            Priority = "Medium"
        };

        var result = TaskValidator.ValidateTask(task);

        Assert.Equal("Title is required.", result);
    }

    [Fact]
    public void ValidateTask_WithInvalidPriority_ReturnsError()
    {
        var task = new TodoTask
        {
            Title = "Test task",
            Description = "Test description",
            Priority = "Extreme"
        };

        var result = TaskValidator.ValidateTask(task);

        Assert.Equal("Priority must be Low, Medium, or High.", result);
    }

    [Fact]
    public void ValidateTask_WithValidTask_ReturnsNull()
    {
        var task = new TodoTask
        {
            Title = "Learn unit testing",
            Description = "Write tests for TaskValidator",
            Priority = "High"
        };

        var result = TaskValidator.ValidateTask(task);

        Assert.Null(result);
    }

    [Fact]
    public void IsValidPriority_WithInvalidPriority_ReturnsFalse()
    {
        string priority = "Extreme";

        bool result = TaskValidator.IsValidPriority(priority);

        Assert.False(result);
    }

    [Theory]
    [InlineData("Low")]
    [InlineData("Medium")]
    [InlineData("High")]
    public void IsValidPriority_WithValidPriority_ReturnsTrue(string priority)
    {
        bool result = TaskValidator.IsValidPriority(priority);

        Assert.True(result);
    }

    [Fact]
public void ValidateTask_WithTitleOver100Characters_ReturnsError()
{
    // Arrange
    var task = new TodoTask
    {
        Title = new string('A', 101),
        Description = "Test description",
        Priority = "Medium"
    };

    // Act
    var result = TaskValidator.ValidateTask(task);

    // Assert
    Assert.Equal("Title cannot be longer than 100 characters.", result);
}

[Fact]
public void ValidateTask_WithDescriptionOver500Characters_ReturnsError()
{
    // Arrange
    var task = new TodoTask
    {
        Title = "Test task",
        Description = new string('A', 501),
        Priority = "Medium"
    };

    // Act
    var result = TaskValidator.ValidateTask(task);

    // Assert
    Assert.Equal("Description cannot be longer than 500 characters.", result);
}
}