# ProductAPI - RESTful Web API (.NET 8)

## Overview

ProductAPI is a RESTful Web API built using ASP.NET Core 8 following Clean Architecture principles.

The application provides CRUD operations for Products and Items using SQL Server and Entity Framework Core.

---

## Technology Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- JWT Authentication
- Swagger / OpenAPI
- Serilog
- xUnit & Moq
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

Architecture Layers

- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

---

# API Endpoints

## Products

| Method | URL | Description |
|---------|-----|-------------|
| GET | /api/v1/products | Get all products |
| GET | /api/v1/products/{id} | Get product by Id |
| POST | /api/v1/products | Create Product |
| PUT | /api/v1/products/{id} | Update Product |
| DELETE | /api/v1/products/{id} | Delete Product |

---

## Items

| Method | URL |
|---------|-----|
| GET | /api/v1/products/{productId}/items |
| POST | /api/v1/products/{productId}/items |
| PUT | /api/v1/items/{id} |
| DELETE | /api/v1/items/{id} |

---

# OpenAPI / Swagger Documentation

Swagger is enabled for testing the REST API.

Run the application and open

```
https://localhost:5001/swagger
```

or

```
https://localhost:7xxx/swagger
```

Swagger provides

- Request Models
- Response Models
- Status Codes
- Authentication
- API Versioning

---

# Authentication Flow (High Level)

The application uses JWT Authentication.

Authentication Process

Client

↓

POST /api/v1/auth/login

↓

Validate User

↓

Generate JWT Token

↓

Return Access Token

↓

Client sends

Authorization: Bearer {token}

↓

Protected API

```
Client
   │
   ▼
Login API
   │
   ▼
Validate Credentials
   │
   ▼
Generate JWT Token
   │
   ▼
Return Token
   │
   ▼
Client Calls Protected APIs
```

---

# Request Validation

FluentValidation is used for request validation.

Example

CreateProductValidator

UpdateProductValidator

CreateItemValidator

UpdateItemValidator

---

# Error Handling

Global Exception Middleware is implemented.

Example Response

```json
{
  "statusCode":500,
  "message":"Internal Server Error"
}
```

---

# Logging

Logging is implemented using Serilog.

Logs are written to

- Console

---

# Environment Setup

## Prerequisites

- Visual Studio 2022
- .NET 8 SDK
- SQL Server Express / SQL Server
- SQL Server Management Studio

---

## Clone Repository

```bash
git clone https://github.com/<username>/ProductAPI.git
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Update Database

```powershell
Add-Migration InitialCreate
```

```powershell
Update-Database
```

---

## Run Application

Visual Studio

```
F5
```

or

```
Ctrl + F5
```

---

# SQL Server

Connection String

```json
"ConnectionStrings": {
"DefaultConnection":
"Server=SHREE-DIVYARAJ\\SQLEXPRESS;
Database=Test;
Trusted_Connection=True;
TrustServerCertificate=True;"
}
```

---

# Deployment Procedure

## Local Deployment

1. Restore Packages
2. Build Solution
3. Update Database
4. Run API
5. Open Swagger

---

## Docker Deployment

Build Image

```bash
docker build -t productapi .
```

Run Container

```bash
docker run -p 8080:80 productapi
```

Docker Compose

```bash
docker compose up
```

---

# Unit Testing

Run Tests

```bash
dotnet test
```

Testing Frameworks

- xUnit
- Moq
- FluentAssertions

---

# Performance

- Async/Await
- AsNoTracking()
- Repository Pattern
- Dependency Injection
- Pagination
- SQL Indexes

---

# Security

- JWT Authentication
- Role Based Authorization
- HTTPS
- CORS
- FluentValidation
- SQL Injection Protection

---

# Author

Vijay Thorat

.NET Full Stack Developer