using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Http.Json;
using TaskManager.Api.Data;

namespace TaskManager.Api.Tests;

public class TaskApiTests
{
    [Fact]
    public async Task GetTasks_ReturnsOk()
    {
        await using var application = CreateApplication("GetTasksDatabase");

        var client = application.CreateClient();

        var response = await client.GetAsync("/tasks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostTask_WithValidData_ReturnsCreated()
    {
        await using var application = CreateApplication("PostValidDatabase");

        var client = application.CreateClient();

        var newTask = new
        {
            title = "Integration test task",
            description = "Created from integration test",
            isCompleted = false,
            priority = "Medium",
            dueDate = (DateTime?)null
        };

        var response = await client.PostAsJsonAsync("/tasks", newTask);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PostTask_WithEmptyTitle_ReturnsBadRequest()
    {
        await using var application = CreateApplication("PostInvalidDatabase");

        var client = application.CreateClient();

        var newTask = new
        {
            title = "",
            description = "Invalid task",
            isCompleted = false,
            priority = "Medium",
            dueDate = (DateTime?)null
        };

        var response = await client.PostAsJsonAsync("/tasks", newTask);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetTask_WithUnknownId_ReturnsNotFound()
    {
        await using var application = CreateApplication("GetUnknownDatabase");

        var client = application.CreateClient();

        var response = await client.GetAsync("/tasks/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static WebApplicationFactory<Program> CreateApplication(string databaseName)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll<TaskDbContext>();
                    services.RemoveAll<DbContextOptions<TaskDbContext>>();
                    services.RemoveAll<IDbContextOptionsConfiguration<TaskDbContext>>();

                    services.AddDbContext<TaskDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(databaseName);
                    });
                });
            });
    }
}