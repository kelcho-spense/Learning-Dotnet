# 🚀 .NET Learning Repository

A comprehensive collection of .NET projects demonstrating various concepts, patterns, and technologies from basic C# fundamentals to advanced ASP.NET Core Web APIs.

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![C#](https://img.shields.io/badge/C%23-12-blue)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Projects Summary](#-projects-summary)
- [Detailed Project Documentation](#-detailed-project-documentation)
  - [Console Applications](#console-applications)
  - [Web APIs](#web-apis)
- [Technologies Used](#-technologies-used)
- [Getting Started](#-getting-started)
- [Learning Path](#-learning-path)

---

## 🎯 Overview

This repository serves as a learning resource for .NET development, covering:

| Category | Topics |
|----------|--------|
| **C# Fundamentals** | Encapsulation, Properties, Exception Handling |
| **Asynchronous Programming** | async/await, Task-based patterns, HttpClient |
| **LINQ** | Query syntax, Method syntax, IQueryable vs IEnumerable |
| **Entity Framework Core** | Code-First, Migrations, Relationships, Fluent API |
| **ASP.NET Core Web API** | REST APIs, Dependency Injection, Swagger/OpenAPI |
| **Caching** | Redis distributed caching, In-memory caching |
| **Design Patterns** | Repository pattern, Service layer, DTO pattern |
| **Object Mapping** | AutoMapper profiles, Transformations |

---

## 📊 Projects Summary

| Project | Framework | Type | Key Focus Areas |
|---------|-----------|------|-----------------|
| [HelloWorld](#1-helloworld) | .NET 10.0 | Console | Encapsulation, Properties, Validation |
| [AsynchronousProgramming](#2-asynchronousprogramming) | .NET 10.0 | Console | Async/Await, HttpClient, TAP |
| [Bank](#3-bank) | .NET 10.0 | Console | Custom Exceptions, Exception Handling |
| [LearningLINQ](#4-learninglinq) | .NET 10.0 | Console | LINQ basics, Filtering, Sorting, Aggregation |
| [IQueryable Workshop](#5-iqueryable-workshop) | .NET 10.0 | Console | IQueryable, LINQ with EF Core |
| [EFCore-WorkShop-CodeFirst](#6-efcore-workshop-codefirst) | .NET 10.0 | Console | EF Core, Code-First, Relationships |
| [Automapper](#7-automapper) | .NET 10.0 | Web | AutoMapper, DTO mapping |
| [Blogging Web API](#8-blogging-web-api) | .NET 10.0 | Web API | REST API, Service pattern, Swagger |
| [ECommerceApp](#9-ecommerceapp) | .NET 10.0 | Web API | EF Core, Order Management, Validation |
| [RedisCachingDemo](#10-rediscachingdemo) | .NET 10.0 | Web API | Redis, Distributed Caching |
| [ShoppingCartAPI](#11-shoppingcartapi) | .NET 10.0 | Web API | DI Lifetimes, In-memory Cache |

---

## 📚 Detailed Project Documentation

### Console Applications

---

#### 1. HelloWorld

> **Path:** `HelloWorld/`

**Purpose:** Demonstrates encapsulation and property validation in C#.

**Concepts Demonstrated:**
- ✅ Encapsulation (private fields, public properties)
- ✅ Property getters and setters with validation
- ✅ Defensive programming with input validation
- ✅ Exception handling for invalid inputs

**Code Highlights:**
```csharp
// Property with validation
public decimal Amount
{
    get => _amount;
    set
    {
        if (value < 0)
            throw new ArgumentException("Amount cannot be negative");
        _amount = value;
    }
}
```

**How to Run:**
```bash
cd HelloWorld
dotnet run
```

---

#### 2. AsynchronousProgramming

> **Path:** `AsynchronousProgramming/`

**Purpose:** Demonstrates asynchronous programming patterns using async/await with HttpClient.

**Concepts Demonstrated:**
- ✅ `async`/`await` keywords
- ✅ HttpClient singleton pattern
- ✅ Task-based Asynchronous Pattern (TAP)
- ✅ Exception handling in async code
- ✅ REST API consumption

**Key Technologies:**
- HttpClient
- System.Threading.Tasks
- JSONPlaceholder API (external test API)

**Best Practices Shown:**
- Reusing HttpClient instances (singleton pattern)
- Proper configuration with BaseAddress, Timeout, Headers
- Async methods returning `Task<T>`

**How to Run:**
```bash
cd AsynchronousProgramming
dotnet run
```

---

#### 3. Bank

> **Path:** `Bank/`

**Purpose:** Demonstrates custom exception handling in C#.

**Concepts Demonstrated:**
- ✅ Creating custom exception classes
- ✅ Overriding `Message` and `HelpLink` properties
- ✅ Exception throwing and catching
- ✅ Accessing exception properties (StackTrace, Source)

**Code Highlights:**
```csharp
public class CustomException : Exception
{
    public override string Message => "Custom error message";
    public override string HelpLink => "https://docs.example.com/help";
}
```

**How to Run:**
```bash
cd Bank
dotnet run
```

---

#### 4. LearningLINQ

> **Path:** `LearningLINQ/`

**Purpose:** Introduction to LINQ with in-memory collections.

**Concepts Demonstrated:**
- ✅ LINQ query syntax and method syntax
- ✅ `Where` - Filtering collections
- ✅ `OrderBy`/`OrderByDescending` - Sorting
- ✅ `Any` - Existence checks
- ✅ `Average`, `Max` - Aggregation functions
- ✅ `First` - Retrieving single elements

**Project Structure:**
```
LearningLINQ/
├── Game.cs         # Model class (Title, Genre, ReleaseYear, Rating, Price)
└── Program.cs      # LINQ examples and demonstrations
```

**Example Operations:**
- Filtering games by genre
- Sorting by rating (ascending/descending)
- Finding the highest-rated game
- Calculating average price

**How to Run:**
```bash
cd LearningLINQ
dotnet run
```

---

#### 5. IQueryable Workshop

> **Path:** `IQueryable Workshop/`

**Purpose:** Comprehensive tutorial on IQueryable and LINQ operations with Entity Framework Core.

**Concepts Demonstrated:**
- ✅ `IQueryable` vs `IEnumerable` differences
- ✅ Deferred execution
- ✅ LINQ operators: `Where`, `OrderBy`, `ThenBy`, `Skip`, `Take`
- ✅ Aggregations: `Count`, `Max`, `Min`
- ✅ `GroupBy` with projections
- ✅ `Any`, `All` operators
- ✅ `FirstOrDefault`

**Key Technologies:**
- Entity Framework Core 10.0.0
- SQL Server

**Project Structure:**
```
IQueryable Workshop/
└── IQueryable Workshop/
    ├── Employee.cs       # Entity model
    ├── AppDbContext.cs   # EF Core context with Fluent API
    ├── SQLQuery.sql      # SQL setup scripts
    └── Program.cs        # 9 example queries
```

**Query Examples Covered:**
1. Basic filtering with `Where`
2. Sorting with `OrderBy` and `ThenBy`
3. Pagination with `Skip` and `Take`
4. Aggregations (`Count`, `Max`, `Min`)
5. Grouping with `GroupBy`
6. Existence checks with `Any` and `All`
7. Anonymous type projections

**How to Run:**
```bash
cd "IQueryable Workshop/IQueryable Workshop"
dotnet run
```

---

#### 6. EFCore-WorkShop-CodeFirst

> **Path:** `EFCore-WorkShop-CodeFirst/`

**Purpose:** Entity Framework Core Code-First workshop demonstrating a blogging platform database design.

**Concepts Demonstrated:**
- ✅ Code-First migrations
- ✅ Entity relationships: One-to-One, One-to-Many, Many-to-Many
- ✅ Fluent API configuration
- ✅ Navigation properties
- ✅ GUID primary keys
- ✅ `Include`/`ThenInclude` for eager loading

**Key Technologies:**
- Entity Framework Core 10.0.0
- SQL Server

**Project Structure:**
```
EFCore-WorkShop-CodeFirst/
├── Entities/
│   ├── User.cs
│   ├── Profile.cs
│   ├── Author.cs
│   ├── Blog.cs
│   ├── Comment.cs
│   └── Category.cs
├── Data/
│   └── AppDbContext.cs   # DbContext with Fluent API
├── Migrations/
└── Program.cs
```

**Relationship Patterns:**
| Relationship | Entities | Type |
|--------------|----------|------|
| User ↔ Profile | 1:1 | One-to-One |
| Author ↔ Blog | 1:M | One-to-Many |
| Blog ↔ Comment | 1:M | One-to-Many (Restrict Delete) |
| Blog ↔ Category | M:M | Many-to-Many (Join Table) |

**How to Run:**
```bash
cd EFCore-WorkShop-CodeFirst
dotnet ef database update  # Apply migrations
dotnet run
```

---

### Web APIs

---

#### 7. Automapper

> **Path:** `Automapper/`

**Purpose:** Comprehensive AutoMapper tutorial demonstrating object-to-object mapping.

**Concepts Demonstrated:**
- ✅ Entity → DTO → ViewModel mapping workflow
- ✅ Property flattening (FirstName + LastName → FullName)
- ✅ Value transformation (DateTime → formatted string)
- ✅ Mapping profiles
- ✅ AutoMapper 15.0+ license configuration

**Key Technologies:**
- AutoMapper 15.1.0
- ASP.NET Core
- Microsoft.Extensions.Logging

**Project Structure:**
```
Automapper/
├── Models/
│   └── Employee.cs           # Database entity
├── DTOs/
│   └── EmployeeDTO.cs        # Data transfer object
├── View/
│   └── EmployeeViewModel.cs  # UI view model
├── EmployeeProfile.cs        # Mapping configuration
└── Program.cs
```

**Mapping Pipeline:**
```
Employee (Entity) → EmployeeDTO → EmployeeViewModel
     ↓                   ↓              ↓
  Database          Service Layer    UI/API Response
```

**How to Run:**
```bash
cd Automapper
dotnet run
```

---

#### 8. Blogging Web API

> **Path:** `Blogging Web API/`

**Purpose:** ASP.NET Core Web API for product management with service layer pattern.

**Concepts Demonstrated:**
- ✅ RESTful API design
- ✅ Dependency Injection (Scoped lifetime)
- ✅ DTO pattern for API contracts
- ✅ Service interface abstraction
- ✅ OpenAPI/Swagger integration
- ✅ Docker containerization

**Key Technologies:**
- ASP.NET Core (.NET 10.0)
- Swashbuckle.AspNetCore 10.1.0
- Microsoft.AspNetCore.OpenApi

**Project Structure:**
```
Blogging Web API/
├── Controllers/
│   └── ProductController.cs
├── Services/
│   ├── IProductService.cs
│   └── ProductService.cs
├── DTOs/
│   ├── ProductDTO.cs
│   ├── ProductCreateDTO.cs
│   └── ProductUpdateDTO.cs
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── Dockerfile
└── Program.cs
```

**API Endpoints:**
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update product |
| DELETE | `/api/products/{id}` | Delete product |

**How to Run:**
```bash
cd "Blogging Web API"
dotnet run
# Or with Docker
docker build -t blogging-api .
docker run -p 5000:5000 blogging-api
```

---

#### 9. ECommerceApp

> **Path:** `ECommerceApp/`

**Purpose:** Complete e-commerce order management system with Entity Framework Core.

**Concepts Demonstrated:**
- ✅ Entity Framework Core (Code-First)
- ✅ Complex entity relationships (1:1, 1:M)
- ✅ Lazy loading proxies
- ✅ Database seeding
- ✅ Swagger/OpenAPI documentation
- ✅ Business validation rules
- ✅ Auditable entities

**Key Technologies:**
- Entity Framework Core 10.0.1
- SQL Server
- EF Core Proxies (Lazy Loading)
- Swashbuckle.AspNetCore

**Project Structure:**
```
ECommerceApp/
├── Controllers/
│   ├── OrderController.cs
│   └── CustomerController.cs
├── Models/
│   ├── BaseAuditableEntity.cs
│   ├── Customer.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Product.cs
│   ├── Address.cs
│   ├── Category.cs
│   └── Profile.cs
├── DTOs/
│   ├── OrderRequestDTO.cs
│   ├── OrderResponseDTO.cs
│   └── CustomerDTO.cs
├── Data/
│   └── AppDbContext.cs
├── Enums/
│   └── OrderStatusEnum.cs
└── Migrations/
```

**Key Features:**
- 🛒 Order placement with stock validation
- 📦 Inventory management
- 🏠 Multi-address support (shipping/billing)
- 📋 Order status tracking
- 🕐 Audit fields (CreatedAt, UpdatedAt)

**How to Run:**
```bash
cd ECommerceApp
dotnet ef database update
dotnet run
```

---

#### 10. RedisCachingDemo

> **Path:** `RedisCachingDemo/`

**Purpose:** Demonstrates distributed caching with Redis in ASP.NET Core.

**Concepts Demonstrated:**
- ✅ Redis distributed cache integration
- ✅ Cache-aside pattern
- ✅ Sliding vs absolute expiration
- ✅ Cache invalidation strategies
- ✅ IConnectionMultiplexer for advanced Redis operations
- ✅ JSON serialization for cache storage

**Key Technologies:**
- StackExchange.Redis
- Microsoft.Extensions.Caching.StackExchangeRedis
- Entity Framework Core 10.0.1
- SQL Server

**Project Structure:**
```
RedisCachingDemo/
├── Controllers/
│   └── ProductController.cs    # API with caching logic
├── Models/
│   └── Product.cs
├── Data/
│   └── AppDbContext.cs         # EF Core context with seed data
└── Migrations/
```

**Caching Patterns Implemented:**

| Pattern | Description |
|---------|-------------|
| Cache-Aside | Check cache → If miss, load from DB → Store in cache |
| Sliding Expiration | Reset TTL on each access |
| Absolute Expiration | Fixed expiration time |
| Cache Invalidation | Remove cache on UPDATE/DELETE |

**Cache Key Conventions:**
```
products:all           # All products
products:category:{id} # Products by category
products:{id}          # Individual product
```

**Prerequisites:**
- Redis server running (locally or Docker)

**How to Run:**
```bash
# Start Redis (Docker)
docker run -d -p 6379:6379 redis

# Run the application
cd RedisCachingDemo
dotnet run
```

---

#### 11. ShoppingCartAPI

> **Path:** `ShoppingCartAPI/`

**Purpose:** Demonstrates Dependency Injection lifetimes (Singleton, Scoped, Transient) with a shopping cart system.

**Concepts Demonstrated:**
- ✅ DI service lifetimes: Singleton, Scoped, Transient
- ✅ In-memory caching (IMemoryCache)
- ✅ HttpContextAccessor for request data
- ✅ Service layer architecture
- ✅ Swagger documentation
- ✅ Docker containerization

**Key Technologies:**
- ASP.NET Core (.NET 10.0)
- Microsoft.Extensions.Caching.Memory
- Swashbuckle.AspNetCore

**Project Structure:**
```
ShoppingCartAPI/
├── Controllers/
│   └── CartController.cs
├── Services/
│   ├── ICartService.cs
│   ├── CartService.cs           # Scoped
│   ├── IDiscountService.cs
│   ├── DiscountService.cs       # Transient
│   ├── IAppConfigService.cs
│   ├── AppConfigService.cs      # Singleton
│   └── CartSummaryService.cs
├── Models/
│   ├── CartItem.cs
│   └── CartSummary.cs
├── DTOs/
├── Dockerfile
└── Program.cs
```

**DI Lifetime Demonstration:**

| Service | Lifetime | Behavior |
|---------|----------|----------|
| AppConfigService | Singleton | Single instance for entire app lifetime |
| CartService | Scoped | New instance per HTTP request |
| DiscountService | Transient | New instance every time it's requested |

**Cart Features:**
- 🛒 Add/Get/Clear cart items
- 💰 Tier-based discount calculation
- 📊 Tax calculation (18% GST)
- 🚚 Dynamic delivery fee based on order amount
- 👤 User identification via headers

**How to Run:**
```bash
cd ShoppingCartAPI
dotnet run
# Or with Docker
docker build -t shopping-cart-api .
docker run -p 5000:5000 shopping-cart-api
```

---

## 🛠 Technologies Used

### Frameworks & Runtime
- .NET 10.0
- ASP.NET Core
- Entity Framework Core

### Databases
- SQL Server
- Redis (Caching)

### Libraries & Packages
- AutoMapper
- Swashbuckle (Swagger/OpenAPI)
- StackExchange.Redis
- Microsoft.Extensions.Caching

### Tools
- Docker
- Entity Framework Core CLI

---

## 🚀 Getting Started

### Prerequisites

1. **.NET SDK** (8.0 or 10.0)
   ```bash
   # Check .NET version
   dotnet --version
   ```

2. **SQL Server** (for EF Core projects)
   - Local installation or Docker:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
   ```

3. **Redis** (for RedisCachingDemo)
   ```bash
   docker run -d -p 6379:6379 redis
   ```

### Running a Project

```bash
# Navigate to project directory
cd <project-name>

# Restore dependencies
dotnet restore

# Apply migrations (if applicable)
dotnet ef database update

# Run the application
dotnet run
```

### Running Web APIs

After running a Web API project, access Swagger UI at:
```
https://localhost:<port>/swagger
```

---

## 📖 Learning Path

Recommended order for learning:

```
1. HelloWorld              → C# basics, encapsulation
         ↓
2. Bank                    → Exception handling
         ↓
3. AsynchronousProgramming → Async/await patterns
         ↓
4. LearningLINQ            → LINQ fundamentals
         ↓
5. IQueryable Workshop     → LINQ with databases
         ↓
6. EFCore-WorkShop         → Entity Framework Core
         ↓
7. Automapper              → Object mapping
         ↓
8. Blogging Web API        → Basic Web API
         ↓
9. ECommerceApp            → Full-featured API
         ↓
10. ShoppingCartAPI        → DI patterns
         ↓
11. RedisCachingDemo       → Caching strategies
```

---

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

---

<p align="center">Made with ❤️ for .NET learning</p>
