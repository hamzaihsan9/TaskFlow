# 🚀 TaskFlow — Professional .NET 6 Clean Architecture API

Welcome to **TaskFlow**, an enterprise-grade RESTful Web API written in **C#** and **.NET 6**, designed using **Clean Architecture** and modern software engineering patterns.

---

## 🏛️ Architecture Overview

The solution adheres strictly to Uncle Bob's **Clean Architecture (Dependency Rule)**: dependencies only point **inwards**.

```
                           ┌────────────────────────┐
                           │      TaskFlow.API      │  (Presentation)
                           └───────────┬────────────┘
                                       │
                         ┌─────────────┴─────────────┐
                         ▼                           ▼
              ┌──────────────────────┐    ┌─────────────────────┐
              │ TaskFlow.Application │    │TaskFlow.Infrastructure│
              └──────────┬───────────┘    └──────────┬──────────┘
                         │                           │
                         └─────────────┬─────────────┘
                                       ▼
                           ┌───────────────────────┐
                           │    TaskFlow.Domain    │  (Core Entities)
                           └───────────────────────┘
```

---

## 📁 Project Structure & Codebase Tour

### 1. 🔵 Domain Layer (`src/TaskFlow.Domain`)
*Zero third-party framework dependencies.* Represents the enterprise business rules.
- **Common**:
  - `BaseEntity.cs`: Base model providing `Id`, `CreatedAt`, and `UpdatedAt` audit fields.
- **Entities**:
  - `TaskItem.cs`: Core business entity with title, status, priority, due date, category relationship.
  - `Category.cs`: Grouping entity with 1-to-many relationship with tasks.
- **Enums**:
  - `TaskItemStatus.cs`: `Pending`, `InProgress`, `Completed`, `Cancelled`.
  - `Priority.cs`: `Low`, `Medium`, `High`, `Critical`.
- **Interfaces**:
  - `IGenericRepository<T>.cs`: Generic repository contract for CRUD operations.
  - `ITaskRepository.cs`: Specific queries (by status, category, eager loading).
  - `ICategoryRepository.cs`: Specific category queries & uniqueness checks.
- **Exceptions**:
  - `NotFoundException.cs`: Typed exception for 404 responses.
  - `BadRequestException.cs`: Typed exception for 400 validation/rule failures.

---

### 2. 🟢 Application Layer (`src/TaskFlow.Application`)
*Orchestrates use cases and business logic using CQRS.*
- **Features/Tasks**:
  - `Commands/CreateTask`: Command & Handler mapping DTO to Entity and persisting.
  - `Commands/UpdateTask`: Updates existing task fields and triggers audit timestamp.
  - `Commands/DeleteTask`: Deletes tasks by ID.
  - `Queries/GetAllTasks`: Returns read-only collection of DTOs.
  - `Queries/GetTaskDetail`: Returns single task with category details.
- **Features/Categories**:
  - Commands & Queries for Category management.
- **Validators**:
  - `CreateTaskValidator.cs`: FluentValidation rules (title length, future date check, priority enum validation).
  - `CreateCategoryValidator.cs`: Validation for category names and lengths.
- **Behaviors**:
  - `ValidationBehavior.cs`: MediatR Pipeline Behavior that automatically intercepts and validates every request before handlers execute.
- **ServiceRegistration.cs**: Clean dependency injection extension method (`AddApplicationServices`).

---

### 3. 🟠 Infrastructure Layer (`src/TaskFlow.Infrastructure`)
*Implements data persistence and external integrations.*
- **Persistence**:
  - `TaskFlowDbContext.cs`: EF Core DbContext with automatic audit timestamp assignment on `SaveChangesAsync`.
  - `Configurations/TaskItemConfiguration.cs`: Fluent API configuration, enum string conversions, and foreign key rules.
  - `Configurations/CategoryConfiguration.cs`: Seed data for default categories (Work, Personal, Learning).
- **Repositories**:
  - `GenericRepository<T>.cs`: Base EF Core repository implementation.
  - `TaskRepository.cs`: Concrete queries with eager loading (`Include`).
  - `CategoryRepository.cs`: Concrete category queries.
- **ServiceRegistration.cs**: Clean dependency injection extension method (`AddInfrastructureServices`).

---

### 4. 🔴 Presentation / API Layer (`src/TaskFlow.API`)
*REST endpoints and HTTP request/response pipeline.*
- **Controllers**:
  - `TasksController.cs`: Thin controller delegating all actions to MediatR.
  - `CategoriesController.cs`: Category endpoints.
- **Middleware**:
  - `ExceptionMiddleware.cs`: Global error handling capturing exceptions and returning structured JSON error payloads.
- **Program.cs**: Application startup, pipeline configuration, SQLite database auto-initialization, and Swagger UI setup.

---

### 5. 🧪 Unit Tests (`tests/TaskFlow.UnitTests`)
- `CreateTaskCommandHandlerTests.cs`: Handler isolation tests with Moq & FluentAssertions.
- `CreateCategoryCommandHandlerTests.cs`: Uniqueness validation tests.
- `CreateTaskValidatorTests.cs`: Direct FluentValidation testing via `TestValidate`.

---

## ⚡ How to Run the Project

### 1. Build the Solution
```bash
cd /home/kali/TaskFlow
dotnet build
```

### 2. Run the Unit Tests
```bash
dotnet test
```

### 3. Start the API
```bash
dotnet run --project src/TaskFlow.API/TaskFlow.API.csproj
```

### 4. Open Swagger UI
Navigate in your browser to:
- `http://localhost:5000/swagger`

---

## 📡 Sample API Requests

### Get All Categories
```bash
curl http://localhost:5000/api/categories
```

### Create a New Task
```bash
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Build a production microservice",
    "description": "Utilize Clean Architecture with C# and .NET",
    "priority": "High",
    "categoryId": 1
  }'
```

### Trigger Validation Error (Demo)
```bash
curl -i -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title": "", "priority": "InvalidPriority"}'
```
