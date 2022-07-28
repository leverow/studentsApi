using System.ComponentModel.DataAnnotations;
namespace studentsApi.Model;

public class Student
{
    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    [Required]
    [Range(0,5)]
    public double Grade { get; set; }

    [Required]
    [Range(typeof(DateTime), "01-01-1950", "07-28-2014",
        ErrorMessage = "Date of Birth Must be between 01-01-1970 and 07-28-2014")]
    public DateTime BirthDate { get; set; }
    
}