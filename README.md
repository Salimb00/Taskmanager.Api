# Task Manager API

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)
![Tests](https://img.shields.io/badge/Tests-13%20Passing-brightgreen)
![CI](https://img.shields.io/github/actions/workflow/status/Salimb00/Taskmanager.Api/dotnet.yml?branch=main)

A REST API for managing tasks, built with ASP.NET Core Minimal APIs and Entity Framework Core. The project focuses on backend fundamentals such as CRUD operations, validation, persistence, automated testing, and continuous integration.

## Features

- Create, read, update, and delete tasks
- Retrieve individual tasks by ID
- Filter tasks by completion status and priority
- Validate titles, descriptions, and priorities
- Persistent storage using SQLite
- Unit tests for validation logic
- Integration tests for API endpoints
- Isolated in-memory database for integration testing
- Automated build and testing with GitHub Actions

## Tech Stack

- **C# / .NET 10**
- **ASP.NET Core Minimal API**
- **Entity Framework Core**
- **SQLite**
- **xUnit**
- **GitHub Actions**

## API Endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/` | Check that the API is running |
| `GET` | `/tasks` | Get all tasks |
| `GET` | `/tasks/{id}` | Get a specific task |
| `GET` | `/tasks?completed=true` | Filter by completion status |
| `GET` | `/tasks?priority=High` | Filter by priority |
| `POST` | `/tasks` | Create a new task |
| `PUT` | `/tasks/{id}` | Update an existing task |
| `DELETE` | `/tasks/{id}` | Delete a task |

## Example Task

```json
{
  "title": "Finish Task Manager API",
  "description": "Complete testing and documentation",
  "isCompleted": false,
  "priority": "High",
  "dueDate": "2026-09-15T00:00:00"
}
```

Valid priorities are:

- `Low`
- `Medium`
- `High`

## Getting Started

### 1. Clone the repository

```bash
git clone git@github.com:Salimb00/Taskmanager.Api.git
cd Taskmanager.Api
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run the API

```bash
cd TaskManager.Api
dotnet run
```

### 4. Run the tests

From the repository root:

```bash
dotnet test
```

## Testing

The project currently contains **13 automated tests**:

- **9 unit tests** covering task validation
- **4 integration tests** covering API behaviour

Integration tests use a separate in-memory database, keeping the real SQLite database isolated from test data.

Tests can be executed with:

```bash
dotnet test
```

## Continuous Integration

GitHub Actions automatically restores dependencies, builds the project, and runs the test suite when changes are pushed to the repository.

This helps ensure that changes do not break existing functionality.

## Project Structure

```text
TaskManager/
├── .github/
│   └── workflows/             # GitHub Actions CI
├── TaskManager.Api/
│   ├── Data/                  # Entity Framework DbContext
│   ├── Migrations/            # Database migrations
│   ├── Models/                # Domain models
│   ├── Services/              # Validation logic
│   └── Program.cs             # API endpoints and configuration
├── TaskManager.Api.Tests/
│   ├── TaskValidatorTests.cs  # Unit tests
│   └── TaskApiTests.cs        # Integration tests
└── TaskManager.slnx
```

## What I Learned

Building this project gave me practical experience with:

- Designing REST endpoints with ASP.NET Core Minimal APIs
- Using Entity Framework Core for database access
- Persisting application data with SQLite
- Separating validation logic from API endpoints
- Writing unit tests with xUnit
- Testing complete HTTP requests with integration tests
- Isolating test data using an in-memory database
- Setting up a CI pipeline with GitHub Actions
- Structuring a .NET project for maintainability