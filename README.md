# Task Management System (.NET 10 + Angular 19)

Implements a **Full-Stack Task Management API** with **Secure JWT Authentication**, **Soft Delete** logic, and an **Instant-Update UI**.

---

## Features

- **Backend:** REST API using **.NET 10** & **Web API**.
- **Database:** Persistence layer via **Entity Framework Core** with **SQL Server**.
- **Security:** Fully secured with **JWT Authentication** and custom **HTTP Interceptors**.
- **Data Integrity:** Professional **Soft Delete** implementation (No data is ever lost).
- **Frontend:** Modern UI built with **Angular 19** (Standalone Components + RxJS).
- **Real-time UX:** Instant UI updates and client-side filtering.
- **Validation:** Server-side and Client-side task validation.

---

## Run Locally

### 1. Prerequisites

- **.NET 10 SDK** installed.
- **Node.js (LTS)** & **Angular CLI** installed.
- **SQL Server** instance running.

### 2. Backend Setup

1. Open `appsettings.json` and update your connection string.
2. Run migrations:

```bash
dotnet ef database update

```

3. Start the API:

```bash
dotnet run

```

_The API will be available at: `https://localhost:7123_`

### 3. Frontend Setup

1. Navigate to the UI folder:

```bash
cd TaskManagerUI

```

2. Install dependencies:

```bash
npm install

```

3. Start the application:

```bash
ng serve

```

_Open your browser at: `http://localhost:4200_`

---

## API Endpoints (Documentation)

### Authentication

**`POST /api/Auth/login`**

- **Request:** `{ "username": "admin", "password": "password" }`
- **Response:** `200 OK` with `{ "token": "<JWT_TOKEN>" }`

---

### Task Operations (Requires JWT)

#### Create Task

**`POST /api/Tasks`**
**Request:**

```json
{
  "title": "Complete Project",
  "description": "Finish the README documentation",
  "userId": "1"
}
```

#### List Tasks

**`GET /api/Tasks`**

- **200 OK** → Returns all active and soft-deleted tasks (sorted by date).

#### Soft Delete Task

**`DELETE /api/Tasks/{id}`**

- **Logic:** Flags `isDeleted = true` in the DB instead of removing the record.

---

## Design Notes & Business Rules

| Action           | Logic Applied            | UI Representation                        |
| ---------------- | ------------------------ | ---------------------------------------- |
| **Addition**     | `unshift()` method       | Task appears instantly at the top        |
| **Deletion**     | `Soft Delete` (Flagging) | Task turns red with "Deleted" badge      |
| **Security**     | `Auth Guard`             | Prevents unauthorized access to `/tasks` |
| **Optimization** | `IQueryable` Filtering   | Efficient SQL execution for large data   |

- **Concurrency:** Uses EF Core's built-in tracking for safe data updates.
- **Clean Code:** Adheres to **SOLID** principles and **Repository Pattern**.

---

## Testing

Run Frontend unit tests with:

```bash
ng test

```

Run Backend tests with:

```bash
dotnet test

```

---

## Contact

**Mohammad Ali Abu-Dalou**

- Mobile: +962790132315
- Email: abudalou.mohammad@gmail.com
- LinkedIn: https://www.linkedin.com/in/mohammad-ali-abudalou/

---
