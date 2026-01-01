using IQueryable_Workshop;

using (AppDbContext context = new AppDbContext())
{
    // Example 1: Filtering and limiting results
    IQueryable<Student> topMaleStudents = context.Students
                                        .Where(x => x.Gender == "Male")
                                        .Take(2);
    Console.WriteLine("Top 2 Students Where Gender = Male");
    Console.WriteLine("=====================================");
    foreach (var s in topMaleStudents)
    {
        Console.WriteLine($"ID: {s.ID}, Name: {s.FirstName} {s.LastName}");
    }

    // Example 2: Sorting with multiple criteria
    IQueryable<Student> sortedStudents = context.Students
                                        .OrderByDescending(x => x.FirstName)
                                        .ThenBy(x => x.LastName)
                                        .Take(5);
    Console.WriteLine("\nTop 5 Students Sorted by FirstName Descending and LastName Ascending");
    Console.WriteLine("======================================================================");
    foreach (var s in sortedStudents)
    {
        Console.WriteLine($"ID: {s.ID}, Name: {s.FirstName} {s.LastName}");
    }

    // Example 3: Count aggregation
    int totalStudents = context.Students.Count();
    Console.WriteLine($"\nTotal Number of Students: {totalStudents}");

    // Example 4: Count with filtering
    int maleCount = context.Students.Count(s => s.Gender == "Male");
    int femaleCount = context.Students.Count(s => s.Gender == "Female");
    Console.WriteLine($"Male Students: {maleCount}");
    Console.WriteLine($"Female Students: {femaleCount}");

    // Example 5: Group by with count
    var genderGroups = context.Students
                            .GroupBy(s => s.Gender)
                            .Select(g => new 
                            { 
                                Gender = g.Key, 
                                Count = g.Count() 
                            })
                            .ToList();
    Console.WriteLine("\nStudents Grouped by Gender:");
    Console.WriteLine("============================");
    foreach (var group in genderGroups)
    {
        Console.WriteLine($"Gender: {group.Gender}, Count: {group.Count}");
    }

    // Example 6: Max and Min operations
    var maxId = context.Students.Max(s => s.ID);
    var minId = context.Students.Min(s => s.ID);
    Console.WriteLine($"\nHighest Student ID: {maxId}");
    Console.WriteLine($"Lowest Student ID: {minId}");

    // Example 7: First and FirstOrDefault
    var firstStudent = context.Students.FirstOrDefault();
    if (firstStudent != null)
    {
        Console.WriteLine($"\nFirst Student: {firstStudent.FirstName} {firstStudent.LastName}");
    }

    // Example 8: Any and All operators
    bool hasMaleStudents = context.Students.Any(s => s.Gender == "Male");
    bool allHaveFirstName = context.Students.All(s => !string.IsNullOrEmpty(s.FirstName));
    Console.WriteLine($"\nHas Male Students: {hasMaleStudents}");
    Console.WriteLine($"All Students Have First Name: {allHaveFirstName}");

    // Example 9: Complex query with multiple operations
    var complexQuery = context.Students
                            .Where(s => s.Gender == "Male")
                            .OrderBy(s => s.LastName)
                            .ThenBy(s => s.FirstName)
                            .Skip(1)
                            .Take(2)
                            .Select(s => new 
                            { 
                                FullName = $"{s.FirstName} {s.LastName}",
                                s.Gender 
                            })
                            .ToList();
    Console.WriteLine("\nComplex Query (Skip first male student, take next 2):");
    Console.WriteLine("======================================================");
    foreach (var item in complexQuery)
    {
        Console.WriteLine($"Name: {item.FullName}, Gender: {item.Gender}");
    }

    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
}
