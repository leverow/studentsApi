
namespace studentsApi.Services;

public interface IStudentService
{
    Task<(bool IsSuccess, string? ErrorMessage)> CreateStudentAsync(Model.Student student);
    Task<(bool IsSuccess, string? ErrorMessage, Entity.Student? student)> GetStudentByIdAsync(Guid id);
    Task<(bool IsSuccess, string? ErrorMessage, List<Entity.Student> studentsList)> GetStudentsAsnyc();
    Task<(bool IsSuccess, string? ErrorMessage)> UpdateStudentAsync(Entity.Student student);
    Task<(bool IsSuccess, string? ErrorMessage)> DeleteStudentAsync(Guid id);
}