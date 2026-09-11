# Task Manager API

A simple REST API for managing tasks, built with ASP.NET Core, Entity Framework Core and SQLite.

## Features

- Create tasks
- Get all tasks
- Get a task by ID
- Update tasks
- Delete tasks
- SQLite persistence
- Entity Framework Core migrations

## Tech Stack

- .NET 10
- ASP.NET Core Minimal API
- Entity Framework Core
- SQLite

## Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/tasks` | Get all tasks |
| GET | `/tasks/{id}` | Get a task by ID |
| POST | `/tasks` | Create a task |
| PUT | `/tasks/{id}` | Update a task |
| DELETE | `/tasks/{id}` | Delete a task |

## Run locally

```bash
dotnet restore
dotnet ef database update
dotnet run