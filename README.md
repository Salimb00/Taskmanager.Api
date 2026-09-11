# Task Manager API

A simple Task Manager REST API built with **ASP.NET Core 10 Minimal API** and **Entity Framework Core (SQLite)**. This project demonstrates CRUD operations, input validation, unit testing, integration testing, and CI with GitHub Actions.

## Features

- Create, read, update, and delete tasks.
- Filter tasks by completion status and priority.
- Input validation for task title, description, and priority.
- SQLite database with Entity Framework Core.
- Unit tests with xUnit.
- Integration tests using an in-memory database.
- Automatic test execution with GitHub Actions.

## Tech Stack

- ASP.NET Core 10 (Minimal API)
- Entity Framework Core
- SQLite
- xUnit
- GitHub Actions

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | API health check |
| GET | `/tasks` | Get all tasks |
| GET | `/tasks/{id}` | Get a task by ID |
| GET | `/tasks?completed=true` | Filter by completion |
| GET | `/tasks?priority=High` | Filter by priority |
| POST | `/tasks` | Create a task |
| PUT | `/tasks/{id}` | Update a task |
| DELETE | `/tasks/{id}` | Delete a task |

## Running the project

### Clone the repository

```bash
git clone <repository-url>
cd TaskManager
```

### Restore packages

```bash
dotnet restore
```

### Run the API

```bash
cd TaskManager.Api
dotnet run
```

The API will start locally and expose the endpoints through ASP.NET Core.

### Run tests

```bash
cd TaskManager.Api.Tests
dotnet test
```

## Testing

The project contains:

- **9 Unit Tests** for validation logic.
- **4 Integration Tests** for API endpoints.
- **13 tests in total**, executed locally and in GitHub Actions.

## CI/CD

A GitHub Actions workflow automatically restores dependencies, builds the solution, and runs all tests on every push to the `main` branch.

## Project Structure

```text
TaskManager/
├── TaskManager.Api/         # ASP.NET Core Minimal API
│   ├── Data/
│   ├── Models/
│   ├── Services/
│   └── Program.cs
├── TaskManager.Api.Tests/   # xUnit unit & integration tests
└── .github/workflows/       # GitHub Actions CI
```

## What I learned

This project was built as a backend portfolio project to practice:

- Building REST APIs with Minimal APIs.
- Working with Entity Framework Core and SQLite.
- Separating validation logic into services.
- Writing unit and integration tests.
- Setting up continuous integration with GitHub Actions.


# Task Manager API

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)
![Tests](https://img.shields.io/badge/Tests-13%20Passing-brightgreen)
![CI](https://img.shields.io/github/actions/workflow/status/Salimb00/Taskmanager.Api/dotnet.yml?branch=main)
![License](https://img.shields.io/badge/License-MIT-blue)