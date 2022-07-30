using System;
using Microsoft.AspNetCore.Mvc;
using studentsApi.Data;
using studentsApi.Model;
using studentsApi.Services;

namespace studentsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController: ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStudentService _service;

    public StudentsController(
        AppDbContext context,
        IStudentService service
    )
    {
        _context = context;
        _service = service;
    }

    private static List<Entity.Student> _students = new List<Entity.Student>()
    {
        new Entity.Student{
            Id = Guid.NewGuid(),
            Name = "Teshaboy",
            Grade = 1,
            BirthDate = DateTime.Parse("12.02.2002")
        }
    };

    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromForm]Model.Student studentModel)
    {
        await _service.CreateStudentAsync(studentModel);
        return Created("api/[controller]", studentModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var result = (await _service.GetStudentsAsnyc());
        if(result.IsSuccess) return Ok(result.studentsList);
        else return BadRequest($"Error occured: {result.ErrorMessage}");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentAsync([FromRoute]Guid id)
    {
        var result = await _service.GetStudentByIdAsync(id);
        if(result.IsSuccess) return Ok(result.student);
        else return BadRequest($"Error occured: {result.ErrorMessage}");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent([FromRoute]Guid id, [FromForm]Model.Student studentModel)
    {
        var student = new Entity.Student()
        {
            Id = id,
            Name = studentModel.Name,
            Grade = studentModel.Grade,
            BirthDate = studentModel.BirthDate
        };
        var result = await _service.UpdateStudentAsync(student);
        if(result.IsSuccess) return Accepted();
        else return BadRequest($"Error occured: {result.ErrorMessage}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute]Guid id)
    {
        var result = await _service.DeleteStudentAsync(id);
        if(result.IsSuccess) return Accepted();
        else return BadRequest($"Error occure: {result.ErrorMessage}");
    }
}