namespace AutomapperTutorial.Models
{
    /// <summary>
    /// Employee Entity - Represents the database model for an employee.
    /// 
    /// This class directly maps to the database table structure and contains
    /// all employee-related information as stored in the database.
    /// 
    /// Mapping Targets:
    /// - Maps to: EmployeeDto (flattens FirstName + LastName into FullName)
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier for the employee. Primary key in the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee's first name. Nullable string.
        /// Combined with LastName to create FullName in EmployeeDto.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Employee's last name. Nullable string.
        /// Combined with FirstName to create FullName in EmployeeDto.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Employee's date of birth. Used for age calculation and formatted display.
        /// Formatted as "yyyy-MM-dd" when mapped to EmployeeDto.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Employee's department assignment. Examples: IT, HR, Finance, Sales.
        /// Passed through to EmployeeDto without modification.
        /// </summary>
        public string? Department { get; set; }
    }
}
