using TaskManager.Api.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Task Manager API is running");

var tasks = new List<TodoTask>
{
    new TodoTask
    {
        Id = 1,
        Title = "Learn ASP.NET Core",
        Description = "Build the first Task Manager API endpoint",
        IsCompleted = false
    },

    new TodoTask
    {
        Id = 2,
        Title = "Update GitHub",
        Description = "Push the latest project changes",
        IsCompleted = true
    }
};

app.MapGet("/tasks", () => tasks);
app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    return task is not null
        ? Results.Ok(task)
        : Results.NotFound();
});
app.MapPut("/tasks/{id}", (int id, TodoTask updatedTask) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
    {
        return Results.NotFound();
    }

    task.Title = updatedTask.Title;
    task.Description = updatedTask.Description;
    task.IsCompleted = updatedTask.IsCompleted;

    return Results.Ok(task);
});

app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
    {
        return Results.NotFound();
    }

    tasks.Remove(task);

    return Results.NoContent();
});

app.MapPost("/tasks", (TodoTask newTask) =>
{
    newTask.Id = tasks.Count + 1;

    tasks.Add(newTask);

    return Results.Created($"/tasks/{newTask.Id}", newTask);
});
app.Run();
