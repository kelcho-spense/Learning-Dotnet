# AutoMapper Tutorial Project

A comprehensive C# project demonstrating AutoMapper for object-to-object mapping, including Entity models, DTOs (Data Transfer Objects), and ViewModels with practical examples.

## Table of Contents

- [Overview](#overview)
- [What is AutoMapper?](#what-is-automapper)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [License Information](#license-information)
- [Architecture & Mapping Patterns](#architecture--mapping-patterns)
- [Key Components](#key-components)
- [Examples](#examples)
- [Running the Application](#running-the-application)

---

## Overview

This project demonstrates the proper use of AutoMapper in a .NET 8 console application. It showcases mapping between three different layers of data representation:

1. **Entity Layer** (`Employee.cs`) - Database model
2. **DTO Layer** (`EmployeeDto.cs`) - Data transfer object for API/service layer
3. **View Layer** (`EmployeeViewModel.cs`) - UI representation model

The project uses AutoMapper 15.1.0 with proper configuration and licensing.

---

## What is AutoMapper?

AutoMapper is a powerful .NET library that automatically maps objects from one type to another. It reduces boilerplate code and makes your application more maintainable.

### Key Benefits:

- **Convention-Based Mapping**: Automatically maps properties with matching names
- **Flattening**: Combines nested properties into a single flat object
- **Custom Transformations**: Supports complex mapping logic via `ForMember()`
- **Type Safety**: Uses generic types for compile-time safety
- **Lazy Loaded**: Only executes mappings when needed
- **Validation**: Can validate mapping configurations at startup

### Why Use AutoMapper?

- Reduces repetitive mapping code
- Improves code maintainability
- Separates concerns between layers
- Makes unit testing easier
- Supports complex transformation scenarios

---

## Project Structure

```
AutomapperTutorial/
├── Models/
│   └── Employee.cs              # Entity model (database representation)
├── DTOs/
│   └── EmployeeDto.cs           # Data Transfer Object
├── View/
│   └── EmployeeViewModel.cs      # ViewModel for UI
├── EmployeeProfile.cs            # AutoMapper configuration/profile
├── Program.cs                    # Application entry point
├── Properties/
│   └── launchSettings.json       # Launch settings
├── appsettings.json              # Application configuration
├── appsettings.Development.json  # Development-specific settings
├── AutomapperTutorial.csproj    # Project file
└── README.md                     # This file
```

---

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- Visual Studio, Visual Studio Code, or any .NET IDE

### Installation

1. **Clone or open the project**
   ```bash
   cd Automapper
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run --project AutomapperTutorial.csproj
   ```

---

## License Information

### AutoMapper 15.1.0 License Requirement

Starting with AutoMapper 15.0, the library requires a valid license key for production use. This is provided by Lucky Penny Software.

#### License Types:

- **Development/Testing**: Free - no license key required (with warning)
- **Production**: Requires a valid license key

#### Obtaining a License:

1. Visit: https://luckypennysoftware.com
2. Register an account
3. Subscribe to a plan (monthly or annual)
4. Copy your license key from the dashboard

#### Adding Your License Key:

In `Program.cs`, replace the license key:

```csharp
configExpression.LicenseKey = "your-actual-license-key-here";
```

#### Best Practices for License Management:

**Option 1: Environment Variable (Recommended)**
```csharp
var licenseKey = Environment.GetEnvironmentVariable("AUTOMAPPER_LICENSE_KEY");
configExpression.LicenseKey = licenseKey;
```

Set the environment variable:
```bash
# Windows PowerShell
[Environment]::SetEnvironmentVariable("AUTOMAPPER_LICENSE_KEY", "your-key", "User")

# Windows Command Prompt
setx AUTOMAPPER_LICENSE_KEY "your-key"

# Linux/macOS
export AUTOMAPPER_LICENSE_KEY="your-key"
```

**Option 2: Configuration File**
```json
{
  "AutoMapper": {
    "LicenseKey": "your-license-key-here"
  }
}
```

In `Program.cs`:
```csharp
var licenseKey = builder.Configuration["AutoMapper:LicenseKey"];
configExpression.LicenseKey = licenseKey;
```

**Option 3: Azure Key Vault (Production)**
```csharp
var licenseKey = await keyVaultClient.GetSecretAsync("automapper-license-key");
configExpression.LicenseKey = licenseKey.Value;
```

#### Current License Status:

✅ **Current License Key**: Valid (expires January 15, 2027)
- Account ID: 019a7312-00da-7bf7-88a6-5d3cc47c40fb
- Customer ID: ctm_01k9sh510vt25ce81p0tv228yw
- Edition: 0
- Type: 2

---

## Architecture & Mapping Patterns

### Layered Architecture

This project follows a **3-layer mapping pattern**:

#### Layer 1: Entity Model
The **Entity** represents data stored in your database.

```csharp
// Employee.cs - Database representation
public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Department { get; set; }
}
```

**Characteristics**:
- Directly maps to database schema
- May contain sensitive information
- Not suitable for external APIs
- Can have navigation properties for related entities

#### Layer 2: Data Transfer Object (DTO)
The **DTO** transfers data between services, combining and transforming data as needed.

```csharp
// EmployeeDto.cs - Service layer representation
public class EmployeeDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }        // Flattened (FirstName + LastName)
    public string? Department { get; set; }
    public string? DateOfBirthFormatted { get; set; }  // Formatted date
}
```

**Characteristics**:
- Combines related fields (flattening)
- Formats data appropriately
- Controls what data is exposed
- Enables API versioning

#### Layer 3: ViewModel
The **ViewModel** provides UI-specific representation, hiding unnecessary details.

```csharp
// EmployeeViewModel.cs - UI representation
public class EmployeeViewModel
{
    public string? FullName { get; set; }
    public string? Department { get; set; }
    public string? DateOfBirthFormatted { get; set; }
}
```

**Characteristics**:
- Contains only UI-needed properties
- Already formatted for display
- Lightweight and focused
- No unnecessary identifiers

### Mapping Flow

```
Employee (Entity)
     ↓
  [Mapper using EmployeeProfile]
     ↓
EmployeeDto (DTO)
     ↓
  [Mapper using EmployeeProfile]
     ↓
EmployeeViewModel (ViewModel)
```

---

## Key Components

### 1. **EmployeeProfile.cs** - Mapping Configuration

Defines how AutoMapper should convert between types:

```csharp
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        // Employee → EmployeeDto mapping
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.DateOfBirthFormatted, 
                opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")));

        // EmployeeDto → EmployeeViewModel mapping
        CreateMap<EmployeeDto, EmployeeViewModel>()
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Department, 
                opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.DateOfBirthFormatted, 
                opt => opt.MapFrom(src => src.DateOfBirthFormatted));
    }
}
```

**Key Features**:
- **Inheritance from Profile**: Organizes related mappings
- **CreateMap<TSource, TDestination>()**: Defines mapping rules
- **ForMember()**: Customizes individual property mapping
- **MapFrom()**: Specifies source for destination property

### 2. **Program.cs** - Application Entry Point & Configuration

Initializes AutoMapper with license and runs the demonstration:

```csharp
static void Main(string[] args)
{
    // Setup logging and configuration
    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    var configExpression = new MapperConfigurationExpression();
    
    // Add license key (required for AutoMapper 15.0+)
    configExpression.LicenseKey = "your-license-key";
    
    // Register mapping profiles
    configExpression.AddProfile<EmployeeProfile>();
    
    // Create MapperConfiguration with logger factory
    var config = new MapperConfiguration(configExpression, loggerFactory);
    _mapper = config.CreateMapper();
    
    // Create and map objects...
}
```

**Important Notes**:
- `MapperConfiguration` requires `ILoggerFactory` parameter (AutoMapper 15.0+)
- Must be created once per AppDomain
- Should be initialized during application startup
- License key is required for production environments

---

## Examples

### Basic Mapping

```csharp
// Create source object
Employee employee = new Employee
{
    Id = 1,
    FirstName = "John",
    LastName = "Doe",
    DateOfBirth = new DateTime(1985, 5, 20),
    Department = "IT"
};

// Map to DTO
EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

// Result: FullName = "John Doe", DateOfBirthFormatted = "1985-05-20"
```

### Chained Mapping

```csharp
// Map from Entity → DTO → ViewModel in sequence
EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
EmployeeViewModel viewModel = _mapper.Map<EmployeeViewModel>(employeeDto);

// Or use ProjectTo for LINQ queries
var viewModels = dbContext.Employees
    .ProjectTo<EmployeeViewModel>(_mapper.ConfigurationProvider)
    .ToList();
```

### Custom Transformations

The `ForMember()` method allows custom transformations:

```csharp
CreateMap<Employee, EmployeeDto>()
    // Concatenate first and last names
    .ForMember(dest => dest.FullName, 
        opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
    
    // Format date
    .ForMember(dest => dest.DateOfBirthFormatted, 
        opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
    
    // Conditional mapping
    .ForMember(dest => dest.Department, 
        opt => opt.Condition(src => src.Department != null));
```

---

## Running the Application

### Standard Execution

```bash
dotnet run --project AutomapperTutorial.csproj
```

### With License Key (Environment Variable)

```bash
# Set environment variable first
$env:AUTOMAPPER_LICENSE_KEY="your-license-key"

# Then run
dotnet run --project AutomapperTutorial.csproj
```

### Build and Run

```bash
# Build
dotnet build

# Run specific executable
./bin/Debug/net8.0/AutomapperTutorial.exe
```

### Expected Output

```
Employee DTO:
ID: 1
Full Name: John Doe
Department: IT
Date of Birth: 1985-05-20

Employee ViewModel:
Full Name: John Doe
Department: IT
Date of Birth: 1985-05-20
```

### Troubleshooting

**Issue**: "AutoMapper license key not valid" error
- **Solution**: Verify your license key is correct at https://luckypennysoftware.com

**Issue**: "MapperConfiguration does not contain a constructor that takes 1 argument"
- **Solution**: Ensure you're using AutoMapper 15.0+ and providing `ILoggerFactory` parameter

**Issue**: Properties not mapping correctly
- **Solution**: Check that property names match or use `ForMember()` for custom mappings

---

## Advanced Topics

### Custom Value Resolvers

```csharp
public class CustomResolver : IValueResolver<Employee, EmployeeDto, string>
{
    public string Resolve(Employee source, EmployeeDto destination, 
        string destMember, ResolutionContext context)
    {
        return $"{source.FirstName} {source.LastName}".ToUpper();
    }
}

// In Profile:
CreateMap<Employee, EmployeeDto>()
    .ForMember(dest => dest.FullName, 
        opt => opt.MapFrom<CustomResolver>());
```

### Reverse Mapping

```csharp
CreateMap<Employee, EmployeeDto>().ReverseMap();
// Now you can map EmployeeDto → Employee automatically
```

### Validation

```csharp
// Validate mapping configuration at startup
config.AssertConfigurationIsValid();
// Throws exception if mappings are misconfigured
```

---

## Dependencies

- **AutoMapper** v15.1.0 - Object-to-object mapping
- **Microsoft.Extensions.Logging** - Logging infrastructure
- **.NET 8.0** - Target framework

## License

This project uses AutoMapper, which requires a license key for production use.

**AutoMapper License**: https://luckypennysoftware.com

**Project Code**: MIT (or your chosen license)

---

## Resources

- [AutoMapper Official Documentation](https://docs.automapper.io/)
- [AutoMapper GitHub](https://github.com/AutoMapper/AutoMapper)
- [Lucky Penny Software License](https://luckypennysoftware.com)
- [Entity Framework Core with AutoMapper](https://docs.automapper.io/en/stable/Dependency-injection.html)

---

## Contributing

Feel free to extend this project with:
- More complex mapping scenarios
- Integration with databases (Entity Framework Core)
- API endpoints demonstrating DTOs
- Unit tests for mappings
- Performance benchmarks

---

## Author

Created as an educational tutorial for AutoMapper in C#/.NET

**Last Updated**: November 12, 2025