using AutoMapper;
using AutomapperTutorial.DTOs;
using AutomapperTutorial.Models;
using AutomapperTutorial.View;

/// <summary>
/// Employee AutoMapper Profile - Defines all mapping configurations for employee-related classes.
/// 
/// This profile centralizes mapping logic between:
/// 1. Employee (Entity) → EmployeeDto (DTO): Data transformation with flattening and formatting
/// 2. EmployeeDto (DTO) → EmployeeViewModel (ViewModel): UI-specific data projection
/// 
/// AutoMapper Concepts Used:
/// - CreateMap: Defines source-to-destination type mapping
/// - ForMember: Customizes individual property mapping
/// - MapFrom: Specifies custom transformation logic
/// - Flattening: Combining multiple source properties into one destination property
/// - Value Transformation: Converting data types and formats
/// 
/// Profile Best Practices:
/// - Keep profiles organized by domain (Employee, Order, etc.)
/// - Register profiles in MapperConfiguration at application startup
/// - Use meaningful names for clarity
/// - Document complex mapping logic
/// </summary>
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        // ============================================================
        // MAPPING 1: Employee (Entity) → EmployeeDto (DTO)
        // ============================================================
        // Purpose: Transform database entity into API/service layer DTO
        // Features:
        //   - Flattens FirstName + LastName into single FullName property
        //   - Formats DateTime to string in ISO 8601 format
        //   - Simplifies the data structure for external consumption
        // ============================================================
        CreateMap<Employee, EmployeeDto>()
            // Flattening Example: Combine two source properties into one
            // Source: Employee.FirstName (string) + Employee.LastName (string)
            // Destination: EmployeeDto.FullName (string)
            // Logic: Concatenate with space separator
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))

            // Value Transformation Example: Format DateTime to string
            // Source: Employee.DateOfBirth (DateTime)
            // Destination: EmployeeDto.DateOfBirthFormatted (string)
            // Logic: Convert to ISO 8601 format (yyyy-MM-dd)
            .ForMember(
                dest => dest.DateOfBirthFormatted,
                opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")));

        // ============================================================
        // MAPPING 2: EmployeeDto (DTO) → EmployeeViewModel (ViewModel)
        // ============================================================
        // Purpose: Project DTO data into UI-specific ViewModel
        // Features:
        //   - Excludes non-UI properties (like Id)
        //   - Passes already-formatted data through
        //   - Optimizes for presentation layer consumption
        // ============================================================
        CreateMap<EmployeeDto, EmployeeViewModel>()
            // Direct property mapping - Uses convention-based mapping
            // These ForMember declarations are optional (properties match by name)
            // but included here for documentation and explicit control
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName))

            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))

            .ForMember(
                dest => dest.DateOfBirthFormatted,
                opt => opt.MapFrom(src => src.DateOfBirthFormatted));
    }
}

/* 
 * AUTOMAPPER DOCUMENTATION REFERENCE
 * ===================================
 * 
 * Key Concepts:
 * 
 * 1. CONVENTION-BASED MAPPING
 *    - AutoMapper automatically maps properties with identical names
 *    - FirstName → FirstName, Department → Department, etc.
 *    - Reduces configuration needed
 * 
 * 2. FLATTENING
 *    - Combining multiple source properties into single destination property
 *    - Example: FirstName + LastName → FullName
 *    - Great for simplifying complex objects
 * 
 * 3. VALUE TRANSFORMATION
 *    - Converting source data to different format/type
 *    - Example: DateTime → formatted string
 *    - Used with MapFrom(src => ...)
 * 
 * 4. PROFILE ORGANIZATION
 *    - Inherit from Profile class
 *    - Define all related mappings in constructor
 *    - Register in MapperConfiguration at startup
 *    - Makes mapping logic maintainable and discoverable
 * 
 * 5. CUSTOM MAPPING LOGIC
 *    - ForMember: Customize specific property mapping
 *    - MapFrom: Provide custom source selector
 *    - Condition: Add conditional mapping logic
 *    - IValueResolver: Complex resolution logic in separate class
 * 
 * License Information:
 * - AutoMapper 15.0+ requires valid license key for production
 * - Development/testing use is free (displays warning)
 * - Obtain license at: https://luckypennysoftware.com
 * - Set license in MapperConfiguration: configExpression.LicenseKey = "...";
 */
