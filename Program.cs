using TaskManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Task Manager API is running");


app.MapGet("/tasks", async (bool? completed, string? priority, TaskDbContext db) =>
{
    var query = db.Tasks.AsQueryable();

    if (completed.HasValue)
    {
        query = query.Where(task => task.IsCompleted == completed.Value);
    }

    if (!string.IsNullOrWhiteSpace(priority))
    {
        query = query.Where(task => task.Priority == priority);
    }

    var tasks = await query.ToListAsync();

    return Results.Ok(tasks);
});

app.MapPut("/tasks/{id}", async (int id, TodoTask updatedTask, TaskDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(updatedTask.Title))
    {
        return Results.BadRequest("Title is required.");
    }

    if (updatedTask.Title.Length > 100)
    {
        return Results.BadRequest("Title cannot be longer than 100 characters.");
    }

    if (updatedTask.Description.Length > 500)
    {
        return Results.BadRequest("Description cannot be longer than 500 characters.");
    }

    if (!IsValidPriority(updatedTask.Priority))
{
    return Results.BadRequest("Priority must be Low, Medium, or High.");
}

    var task = await db.Tasks.FindAsync(id);

    if (task is null)
    {
        return Results.NotFound();
    }

    task.Title = updatedTask.Title;
    task.Description = updatedTask.Description;
    task.IsCompleted = updatedTask.IsCompleted;
    task.Priority = updatedTask.Priority;
    task.DueDate = updatedTask.DueDate;

await db.SaveChangesAsync();

    return Results.Ok(task);
});

app.MapDelete("/tasks/{id}", async (int id, TaskDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);

    if (task is null)
    {
        return Results.NotFound();
    }

    db.Tasks.Remove(task);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/tasks", async (TodoTask newTask, TaskDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(newTask.Title))
    {
        return Results.BadRequest("Title is required.");
    }

    if (newTask.Title.Length > 100)
    {
        return Results.BadRequest("Title cannot be longer than 100 characters.");
    }

    if (newTask.Description.Length > 500)
    {
        return Results.BadRequest("Description cannot be longer than 500 characters.");
    }

    db.Tasks.Add(newTask);

    if (!IsValidPriority(newTask.Priority))
    {
    return Results.BadRequest("Priority must be Low, Medium, or High.");
    }

    await db.SaveChangesAsync();

    return Results.Created($"/tasks/{newTask.Id}", newTask);
});

static bool IsValidPriority(string priority)
{
    return priority is "Low" or "Medium" or "High";
}

app.Run();
