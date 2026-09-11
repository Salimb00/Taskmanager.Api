using TaskManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Root endpoint
app.MapGet("/", () => "Task Manager API is running");

// GET all tasks + optional filters
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

// GET a single task by ID
app.MapGet("/tasks/{id}", async (int id, TaskDbContext db) =>
{
    var task = await db.Tasks.FindAsync(id);

    if (task is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(task);
});

// UPDATE a task
app.MapPut("/tasks/{id}", async (int id, TodoTask updatedTask, TaskDbContext db) =>
{
    var validationError = TaskValidator.ValidateTask(updatedTask);

    if (validationError is not null)
    {
        return Results.BadRequest(validationError);
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

// DELETE a task
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

// CREATE a task
app.MapPost("/tasks", async (TodoTask newTask, TaskDbContext db) =>
{
    var validationError = TaskValidator.ValidateTask(newTask);

    if (validationError is not null)
    {
        return Results.BadRequest(validationError);
    }

    db.Tasks.Add(newTask);

    await db.SaveChangesAsync();

    return Results.Created($"/tasks/{newTask.Id}", newTask);
});

app.Run();

// Makes Program accessible to integration tests
public partial class Program { }