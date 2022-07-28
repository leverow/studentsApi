using System;
using Microsoft.AspNetCore.Mvc;
using studentsApi.Model;

namespace studentsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController: ControllerBase
{
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
    public IActionResult CreateStudent([FromForm]Model.Student studentModel)
    {
        var student = new Entity.Student()
        {
            Id = Guid.NewGuid(),
            Name = studentModel.Name,
            Grade = studentModel.Grade,
            BirthDate = studentModel.BirthDate
        };
        _students.Add(student);
        return Created("api/[controller]", student);
    }

    [HttpGet]
    public IActionResult GetStudents()
        => Ok(_students);

    [HttpGet("{id}")]
    public IActionResult GetStudent([FromRoute]Guid id)
    {
        var student = _students.FirstOrDefault(b => b.Id == id);
        if(student == default) return NotFound("Student not found");
        return Ok(student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent([FromRoute]Guid id, [FromForm]Model.Student student)
    {
        var old = _students.FirstOrDefault(b => b.Id == id);
        if(old == default) return NotFound("Student not found");
        old.Name = student.Name;
        old.Grade = student.Grade;
        old.BirthDate = student.BirthDate;
        return Accepted();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent([FromQuery]Guid id)
    {
        var student = _students.FirstOrDefault(b => b.Id == id);
        if(student == default) return NotFound("Student not found");
        _students.Remove(student);
        return Accepted();
    }
}