namespace studentsApi.Entity;

public class Student
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Grade { get; set; }
    public DateTime BirthDate { get; set; }
    
}