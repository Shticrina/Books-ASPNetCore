# 📚 Library Management System

A modern **Library Management System** built with **ASP.NET Core Web API** and **Blazor WebAssembly**.

This project was created as a learning exercise to explore clean application architecture, REST APIs, Entity Framework Core, and modern frontend development with Blazor.

The backend is designed as a standalone REST API that can later be consumed by multiple clients (Blazor, React, MAUI, mobile apps, etc.).

---

# ✨ Features

## Backend

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQLite database
- Swagger / OpenAPI documentation
- Dependency Injection
- DTO pattern
- Service Layer pattern
- Global exception handling middleware
- Structured logging with `ILogger<T>`
- Model validation using Data Annotations
- Business validation
    - Author existence validation
    - Category existence validation
    - Entity existence validation
- Consistent API response model
- RFC7807 validation responses
- Clean separation of responsibilities

## Frontend

- Blazor WebAssembly
- API service layer
- Shared DTOs
- Create/List pages
- Server-side validation displayed inside Blazor forms
- Reusable API response handling
- Reusable page components

---

# 🏗 Solution Structure

```
LibraryManagement
│
├── Client
│   ├── Pages
│   ├── Components
│   ├── Services
│   ├── Interfaces
│   └── Exceptions
│
├── Server
│   ├── Controllers
│   ├── Services
│   ├── Interfaces
│   ├── Models
│   ├── Middleware
│   └── Data
│
└── Shared
    ├── DTOs
    └── Responses
```

---

# 🏛 Architecture

```
Blazor WebAssembly
        │
        │ HTTP
        ▼
 ASP.NET Core Web API
        │
        ▼
 Service Layer
        │
        ▼
Entity Framework Core
        │
        ▼
      SQLite
```

Only the **Server** communicates with the database.

The **Client** communicates exclusively through HTTP.

The **Shared** project contains DTOs and response models used by both projects.

---

# 📦 Technologies

- .NET 10
- ASP.NET Core Web API
- Blazor WebAssembly
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Dependency Injection
- C#
- REST API

---

# 📂 Current Project Structure

## Server

```
Controllers
│
├── BooksController
├── AuthorsController
└── CategoriesController

Services
│
├── BookService
├── AuthorService
└── CategoryService

Interfaces
│
├── IBookService
├── IAuthorService
└── ICategoryService

Models
│
├── Book
├── Author
└── Category

Middleware
│
└── ExceptionMiddleware

Data
│
└── AppDbContext
```

---

## Client

```
Pages
Components
Services
Interfaces
Exceptions
```

---

## Shared

```
DTOs
│
├── Books
├── Authors
└── Categories

Responses
│
├── ApiResponse<T>
└── ValidationProblem
```

---

# 🗄 Database Design

## Author

| Field |
|-------|
| Id |
| Name |
| Bio |

One Author → Many Books

---

## Category

| Field |
|-------|
| Id |
| Name |

One Category → Many Books

---

## Book

| Field |
|-------|
| Id |
| Title |
| Isbn |
| PublishedYear |
| AuthorId |
| CategoryId |

Each Book belongs to:

- one Author
- one Category

---

# 📖 API Design

The API follows a Service Layer architecture.

```
Request

↓

Controller

↓

Service

↓

Entity Framework

↓

SQLite
```

Controllers contain **no database logic**.

Services contain:

- Business rules
- Validation
- Database access
- DTO mapping

---

# ✅ Validation

The project currently supports:

## Data Annotation validation

Examples:

- Required
- StringLength
- Range

---

## Business validation

Examples:

- Author exists
- Category exists
- Book exists

---

## Client validation

The Blazor application displays validation messages returned by the API directly inside the form fields.

---

# 🚨 Exception Handling

Global exception handling middleware converts exceptions into consistent API responses.

Examples:

- NotFoundException
- BadRequestException
- Unexpected server errors

---

# 📄 API Responses

Successful responses use a generic wrapper.

Example:

```json
{
  "success": true,
  "statusCode": 200,
  "message": "Book created successfully.",
  "data": {
      ...
  }
}
```

Validation failures use the default ASP.NET Core RFC7807 response.

Example:

```json
{
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
      "Title": [
          "The Title field is required."
      ]
  }
}
```

---

# 📝 Logging

The application uses the built-in `ILogger<T>` infrastructure.

Operations such as:

- retrieving books
- creating books
- updating books
- deleting books
- missing entities

are logged during execution.

---

# 🚀 Getting Started

## Clone

```bash
git clone https://github.com/Shticrina/Books-ASPNetCore
```

---

## Restore packages

```bash
dotnet restore
```

---

## Run migrations

```bash
dotnet ef database update
```

---

## Start the Server

```bash
dotnet run --project Server
```

Swagger:

```
https://localhost:7188/swagger
```

---

## Start the Client

```bash
dotnet run --project Client
```

Blazor:

```
https://localhost:7124
```

---

# 📌 Roadmap

- [x] CRUD API
- [x] DTO pattern
- [x] Service Layer
- [x] Validation
- [x] Logging
- [x] Global exception handling
- [x] Blazor WebAssembly client
- [x] API service layer
- [x] Server-side validation in forms
- [x] Edit Book page
- [x] Delete confirmation dialog
- [x] Book Details page
- [x] Author pages
- [x] Category pages
- [x] Tailwind CSS
- [ ] Dashboard
- [ ] Search & filtering
- [ ] Pagination
- [ ] Authentication & Authorization
- [ ] Repository pattern (optional)
- [ ] Unit testing

---

# 🎯 Purpose

This project is intended as a portfolio piece and learning project demonstrating:

- Clean Architecture principles
- ASP.NET Core Web API
- Blazor WebAssembly
- Entity Framework Core
- REST API development
- Modern C# practices
- Separation of concerns
- Client/server communication
- Validation and error handling

---

## License

This project is licensed under the MIT License.