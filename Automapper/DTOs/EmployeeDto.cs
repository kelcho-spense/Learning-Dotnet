namespace AutomapperTutorial.DTOs
{
    /// <summary>
    /// Employee Data Transfer Object (DTO) - Represents the data transfer layer.
    /// 
    /// This class is used for transferring employee data between services and APIs.
    /// It combines and transforms data from the Employee entity:
    /// - FirstName + LastName are combined into FullName (flattening)
    /// - DateOfBirth is formatted as a string in "yyyy-MM-dd" format
    /// - Excludes unnecessary properties from the entity
    /// 
    /// Source: Employee (Entity Model)
    /// Target: EmployeeViewModel (View Model)
    /// 
    /// Mapping Pattern:
    /// Employee.FirstName + Employee.LastName → EmployeeDto.FullName
    /// Employee.DateOfBirth → EmployeeDto.DateOfBirthFormatted (as "yyyy-MM-dd")
    /// Employee.Department → EmployeeDto.Department (direct copy)
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Unique identifier for the employee. Copied from Employee.Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the employee. Created by combining Employee.FirstName and Employee.LastName.
        /// This is an example of property flattening - combining multiple source properties into one.
        /// Example: "John Doe"
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Employee's department assignment.
        /// Directly mapped from Employee.Department.
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// Employee's date of birth in string format. Created from Employee.DateOfBirth.
        /// Format: "yyyy-MM-dd" (ISO 8601 format)
        /// Example: "1985-05-20"
        /// 
        /// This is an example of value transformation - converting DateTime to formatted string.
        /// </summary>
        public string? DateOfBirthFormatted { get; set; }
    }
}
