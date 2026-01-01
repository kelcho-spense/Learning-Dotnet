using AutoMapper;
using AutomapperTutorial.DTOs;
using AutomapperTutorial.Models;
using AutomapperTutorial.View;

/// <summary>
/// AutoMapper Tutorial - Demonstrates object-to-object mapping with AutoMapper.
/// 
/// This application demonstrates the complete mapping workflow:
/// 1. Create Entity model (Employee) - represents database data
/// 2. Map to DTO (EmployeeDto) - transfer data with transformations
/// 3. Map to ViewModel (EmployeeViewModel) - prepare for UI display
/// 
/// AutoMapper Configuration:
/// - Version: 15.1.0 (requires license key for production)
/// - Framework: .NET 8.0
/// - Logging: Console logging via Microsoft.Extensions.Logging
/// 
/// License Information:
/// - AutoMapper 15.0+ requires a valid license key for production use
/// - Development/testing is free but displays a warning
/// - License registration: https://luckypennysoftware.com
/// 
/// Key Components:
/// - EmployeeProfile: Defines all mapping rules
/// - Employee: Entity model (database)
/// - EmployeeDto: Data transfer object
/// - EmployeeViewModel: UI view model
/// </summary>
class Program
{
    /// <summary>
    /// Static IMapper instance. Should be created once per AppDomain.
    /// In production, this would typically be injected via dependency injection.
    /// </summary>
    private static IMapper? _mapper;

    /// <summary>
    /// Application entry point. Demonstrates complete AutoMapper workflow.
    /// 
    /// Steps:
    /// 1. Initialize logging factory
    /// 2. Configure AutoMapper with profiles and license
    /// 3. Create mapper instance
    /// 4. Create sample entity
    /// 5. Map through layers (Entity → DTO → ViewModel)
    /// 6. Display results
    /// </summary>
    static void Main(string[] args)
    {
        // ============================================================
        // STEP 1: Initialize AutoMapper Configuration
        // ============================================================
        // Create logger factory for diagnostics and error reporting
        // In production, this would be injected from DI container
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        // Create mapper configuration expression
        var configExpression = new MapperConfigurationExpression();

        // ============================================================
        // STEP 2: Set AutoMapper License (Required for AutoMapper 15.0+)
        // ============================================================
        // IMPORTANT: For production deployment, use one of these approaches:
        // 
        // Option A: Environment Variable (Recommended)
        //   var licenseKey = Environment.GetEnvironmentVariable("AUTOMAPPER_LICENSE_KEY");
        //   configExpression.LicenseKey = licenseKey;
        //
        // Option B: Configuration File
        //   var licenseKey = configuration["AutoMapper:LicenseKey"];
        //   configExpression.LicenseKey = licenseKey;
        //
        // Option C: Azure Key Vault (Production)
        //   var licenseKey = await keyVaultClient.GetSecretAsync("automapper-license");
        //   configExpression.LicenseKey = licenseKey.Value;
        //
        // Current approach: Direct string (development/demo only)
        configExpression.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzk0MzU1MjAwIiwiaWF0IjoiMTc2Mjg2NzE4NCIsImFjY291bnRfaWQiOiIwMTlhNzMxMjAwZGE3YmY3ODhhNjVkM2NjNDdjNDBmYiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazlzaDUxMHZ0MjVjZTgxcDB0djIyOHl3Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.P8lFhXbHdB94O8QLFnwseIHsvcpP3xR7yEnJXM7z-Oi-jEO_kwVM5d5uB0vXrP6uGa91nPz6Bx7uVdnh7xQp0lXyFHXWoXRNAfwQn27CnxCeQz5lhvDOJiqwOd1XlWysFp2ZaotoEuF88R4kMC-WP1IPKJr7Pt2gaJURYOysav3xxG2rfevX_a9XNXz2uZx5RCivu0Q5IDPKjguk8xYMTNsUFzri_twv2NID5fWDkyA9lqlBg2bi33xxiZw9YnLXUbqxD7FP_7QtxrawvrR2J2GFg8CaIod3KRahnMzFiFS0co6N5vBB1KyPejUhhp42bKV8jcPx4bcOE2GNxY0CRQ";  // Get your license at https://luckypennysoftware.com

        // ============================================================
        // STEP 3: Register Mapping Profiles
        // ============================================================
        // AddProfile registers the EmployeeProfile which contains
        // all mapping rules for Employee, EmployeeDto, and EmployeeViewModel
        configExpression.AddProfile<EmployeeProfile>();

        // ============================================================
        // STEP 4: Create MapperConfiguration and Mapper
        // ============================================================
        // Note: AutoMapper 15.0+ requires ILoggerFactory as second parameter
        // This is a breaking change from earlier versions
        var config = new MapperConfiguration(configExpression, loggerFactory);
        _mapper = config.CreateMapper();

        Console.WriteLine("=== AutoMapper Tutorial ===\n");

        // ============================================================
        // STEP 5: Create Sample Entity (Database Model)
        // ============================================================
        // Create an Employee entity representing database data
        Employee employee = new Employee
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1985, 5, 20),
            Department = "IT"
        };

        Console.WriteLine("1. Source Entity (Employee):");
        Console.WriteLine($"   FirstName: {employee.FirstName}");
        Console.WriteLine($"   LastName: {employee.LastName}");
        Console.WriteLine($"   DateOfBirth: {employee.DateOfBirth}");
        Console.WriteLine($"   Department: {employee.Department}\n");

        // ============================================================
        // STEP 6a: Map Entity → DTO
        // ============================================================
        // AutoMapper transforms:
        // - FirstName + LastName → FullName (flattening)
        // - DateOfBirth → DateOfBirthFormatted (value transformation)
        EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

        Console.WriteLine("2. Mapped to DTO (EmployeeDto):");
        Console.WriteLine($"   ID: {employeeDto.Id}");
        Console.WriteLine($"   Full Name: {employeeDto.FullName} (flattened from FirstName + LastName)");
        Console.WriteLine($"   Department: {employeeDto.Department}");
        Console.WriteLine($"   Date of Birth: {employeeDto.DateOfBirthFormatted} (formatted as yyyy-MM-dd)\n");

        // ============================================================
        // STEP 6b: Map DTO → ViewModel
        // ============================================================
        // AutoMapper projects DTO data to UI-specific ViewModel
        // Excludes unnecessary properties like Id
        EmployeeViewModel employeeViewModel = _mapper.Map<EmployeeViewModel>(employeeDto);

        Console.WriteLine("3. Mapped to ViewModel (EmployeeViewModel):");
        Console.WriteLine($"   Full Name: {employeeViewModel.FullName}");
        Console.WriteLine($"   Department: {employeeViewModel.Department}");
        Console.WriteLine($"   Date of Birth: {employeeViewModel.DateOfBirthFormatted}\n");

        Console.WriteLine("=== Mapping Complete ===");
        Console.WriteLine("\nKey Takeaways:");
        Console.WriteLine("• AutoMapper automates object transformation");
        Console.WriteLine("• Flattening combines multiple properties into one");
        Console.WriteLine("• Value transformation converts data types and formats");
        Console.WriteLine("• Profiles organize mapping logic by domain");
        Console.WriteLine("• License key required for AutoMapper 15.0+ production use");
    }
}
