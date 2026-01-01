namespace AutomapperTutorial.View
{
    /// <summary>
    /// Employee ViewModel - Represents the UI-specific data model.
    /// 
    /// This class is designed specifically for display in the user interface.
    /// It contains only the properties needed for UI rendering and excludes
    /// unnecessary information like database IDs.
    /// 
    /// Source: EmployeeDto (Data Transfer Object)
    /// 
    /// Display Properties:
    /// - FullName: Employee's complete name (already formatted from DTO)
    /// - Department: Employee's department assignment
    /// - DateOfBirthFormatted: Employee's date of birth in human-readable format
    /// 
    /// Note: The Id property is intentionally excluded as it's not needed for UI display.
    /// </summary>
    public class EmployeeViewModel
    {
        /// <summary>
        /// Employee's full name for display.
        /// Mapped directly from EmployeeDto.FullName (already formatted).
        /// Example: "John Doe"
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Employee's department for display.
        /// Mapped directly from EmployeeDto.Department.
        /// Example: "IT", "HR", "Finance"
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// Employee's date of birth in human-readable format.
        /// Mapped directly from EmployeeDto.DateOfBirthFormatted.
        /// Format: "yyyy-MM-dd"
        /// Example: "1985-05-20"
        /// </summary>
        public string? DateOfBirthFormatted { get; set; }
    }
}
