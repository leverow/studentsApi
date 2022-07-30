using System.ComponentModel.DataAnnotations;

namespace studentsApi.Entity;

public class Student
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Grade { get; set; }
    public DateTime BirthDate { get; set; }
    
}