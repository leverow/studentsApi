using Microsoft.EntityFrameworkCore;
using studentsApi.Data;

namespace studentsApi.Services;

public class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;
    private readonly AppDbContext _context;

    public StudentService(
        ILogger<StudentService> logger,
        AppDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }
    public async Task<(bool IsSuccess, string? ErrorMessage)> CreateStudentAsync(Model.Student studentModel)
    {
        try
        {
            var student = new Entity.Student()
            {
                Id = Guid.NewGuid(),
                Name = studentModel.Name,
                Grade = studentModel.Grade,
                BirthDate = studentModel.BirthDate
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Student successfully added");
            return (true, string.Empty);
        }
        catch(Exception e)
        {
            _logger.LogInformation($"Student wasn't added. Reason: {e.Message}");
            return (false, e.Message);
        }

    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteStudentAsync(Guid id)
    {
        try
        {
            var student = (await GetStudentByIdAsync(id)).student;
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Student successfully deleted");
            return (true, string.Empty);
        }
        catch(Exception e)
        {
            _logger.LogInformation($"Student wasn't deleted. Reason: {e.Message}");
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage, Entity.Student? student)> GetStudentByIdAsync(Guid id)
    {        
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if(student == default) return (false, "Student not found", new Entity.Student());
        else return (true, string.Empty, student);
    }

    public async Task<(bool IsSuccess, string? ErrorMessage, List<Entity.Student> studentsList)> GetStudentsAsnyc()
    {
        var result = _context.Students.ToList();
        if(result.Count == 0) return (false, "Students list is empty", new List<Entity.Student>());
        _logger.LogInformation("Students list received");
        return (true, string.Empty, result);
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateStudentAsync(Entity.Student student)
    {
        try
        {
            var result = _context.Students.Update(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Student successfully updated");
            return (true, string.Empty);
        }
        catch(Exception e)
        {
            _logger.LogInformation($"Student wasn't updated. Reason {e.Message}");
            return (false, e.Message);
        }
    }
}