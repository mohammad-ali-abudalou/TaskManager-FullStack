# Task Manager - Backend API (.NET 10)

This is the **Server-Side** architecture for the Task Management System, built with **ASP.NET Core Web API** to provide secure, scalable, and high-performance task management services.

---

## Backend Architecture

- **Framework:** .NET 10 (C#).
- **Architecture:** **Repository Pattern** ensuring a clean separation between data access and business logic.
- **Database:** Persistence layer using **Entity Framework Core** with **SQL Server**.
- **Data Transfer:** Optimized **DTOs (Data Transfer Objects)** to minimize payload size and hide sensitive model data.

---

## Security & Logic

- **JWT Authentication:** All endpoints (except Login/Register) are protected via **JSON Web Tokens**.
- **Soft Delete Implementation:** Professional data handling where records are flagged with `isDeleted = true` instead of hard removal, preserving data history.
- **Validation:** Server-side integrity checks using **FluentValidation** to ensure clean data entry.
- **Dependency Injection:** Fully decoupled services managed via the built-in .NET DI Container.

---

## Getting Started

1. **Configuration:** Update the connection string in `appsettings.json`.
2. **Database:** Apply migrations to your SQL Server instance:
   ```bash
   dotnet ef database update
Execution: Run the API:

Bash
dotnet run

Design Principles
SOLID Principles: Adhered to for maintainability.

Async/Await: Fully asynchronous database operations for non-blocking I/O.

Global Exception Handling: Centralized middleware for consistent API error responses.


---

## Contact

**Mohammad Ali Abu-Dalou**

- Mobile: +962790132315
- Email: abudalou.mohammad@gmail.com
- Github: https://github.com/mohammad-ali-abudalou/
- LinkedIn: https://www.linkedin.com/in/mohammad-ali-abudalou/

---
