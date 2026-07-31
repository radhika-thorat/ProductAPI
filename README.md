# ProductAPI - RESTful Web API (.NET 8)

## Overview

ProductAPI is a RESTful Web API built using **ASP.NET Core 8**, following **Clean Architecture** principles.

The application provides CRUD operations for **Products** using **SQL Server** and **Entity Framework Core**. It also includes JWT Authentication, API Versioning, Swagger documentation, logging, validation, unit testing, and Docker support.

---

# Technology Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- JWT Authentication
- Swagger / OpenAPI
- Serilog
- xUnit
- Moq
- FluentAssertions
- Docker

---

# Project Architecture

```
Solution
│
├── ProductAPI
├── ProductApplication
├── ProductDomain
├── Infrastructure
├── Services
└── Tests
```

## Architecture Layers

- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

---

# Features

- CRUD Operations for Products
- Item Management
- Repository Pattern
- Unit of Work Pattern
- Dependency Injection
- JWT Authentication
- Role-Based Authorization
- API Versioning
- Swagger Documentation
- FluentValidation
- Global Exception Handling
- Serilog Logging
- Entity Framework Core
- SQL Server Integration
- Docker Support
- Unit & Integration Testing

---

# API Endpoints

## Product APIs

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/v1/products` | Get all products |
| GET | `/api/v1/products/{id}` | Get product by ID |
| POST | `/api/v1/products` | Create a new product |
| PUT | `/api/v1/products/{id}` | Update an existing product |
| DELETE | `/api/v1/products/{id}` | Delete a product |

---

# API Documentation (Swagger)

Swagger is enabled for testing and documenting the REST API.

Open the following URL after running the application:

```
https://localhost:5001/swagger
```

or

```
https://localhost:7xxx/swagger
```

Swagger provides:

- API Documentation
- Request Models
- Response Models
- HTTP Status Codes
- JWT Authentication Support
- API Versioning

---

# Authentication Flow

The application uses **JWT (JSON Web Token) Authentication**.

```
Client
   │
   ▼
POST /api/v1/auth/login
   │
   ▼
Validate Credentials
   │
   ▼
Generate JWT Token
   │
   ▼
Return Access Token
   │
   ▼
Client Sends

Authorization: Bearer {token}

   │
   ▼
Protected APIs
```

---

# Request Validation

Validation is implemented using **FluentValidation**.

Validators include:

- CreateProductValidator
- UpdateProductValidator
---

# Error Handling

Global Exception Handling Middleware is implemented to return consistent API responses.

Example:

```json
{
  "statusCode": 500,
  "message": "Internal Server Error"
}
```

---

# Logging

Logging is implemented using **Serilog**.

Logs are written to:

- Console
- Log Files (`Logs/log-.txt`)

---

# Environment Setup

## Prerequisites

- Visual Studio 2022
- .NET 8 SDK
- SQL Server / SQL Server Express
- SQL Server Management Studio (SSMS)

---

# Clone Repository

```bash
git clone [https://github.com/radhika-thorat/ProductAPI)]
```

---

# Restore NuGet Packages

```bash
dotnet restore
```

---

# Database Migration

Create Migration

```powershell
Add-Migration InitialCreate
```

Update Database

```powershell
Update-Database
```

---

# Run the Application

Using Visual Studio

```
F5
```

or

```
Ctrl + F5
```

Using .NET CLI

```bash
dotnet run
```

---

# SQL Server Configuration

Example Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SHREE-DIVYARAJ\\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

# Deployment

## Local Deployment

1. Restore NuGet Packages
2. Build the Solution
3. Apply Database Migrations
4. Run the API
5. Open Swagger UI

---

## Docker Deployment

Build Docker Image

```bash
docker build -t productapi .
```

Run Docker Container

```bash
docker run -p 8080:80 productapi
```

Using Docker Compose

```bash
docker compose up
```

---

# Running Tests

Execute all tests:

```bash
dotnet test
```

Testing Frameworks:

- xUnit
- Moq
- FluentAssertions

---

# Performance Optimizations

- Async/Await Programming
- AsNoTracking()
- Repository Pattern
- Dependency Injection
- Pagination
- SQL Index Optimization

---

# Security

- JWT Authentication
- Role-Based Authorization
- HTTPS Enforcement
- CORS Policy
- FluentValidation
- SQL Injection Protection
- Security Headers

---

# Project Structure

```
ProductAPI
│
├── Controllers
├── Extensions
├── Middleware
├── ProductApplication
│   ├── DTOs
│   ├── Interfaces
│   └── Validators
│
├── ProductDomain
│   ├── Entities
│   ├── Events
│   ├── Exceptions
│   └── Enums
│
├── Infrastructure
│   ├── Data
│   ├── Identity
│   └── Repositories
│
├── Services
│
└── Tests
```

---

# Author

**Radhika Thorat**

.NET Developer

### Skills

- ASP.NET Core
- .NET 8
- C#
- Entity Framework Core
- SQL Server
- Angular
- JWT Authentication
- REST API Development
- Azure
- Docker
- Clean Architecture
- Design Patterns
