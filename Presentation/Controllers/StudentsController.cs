using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public StudentsController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto studentDto)
    {
        if (studentDto == null) return BadRequest("Student data is required");
        var createdStudentDto = await _libraryService.CreateStudentAsync(studentDto);
        return Ok(createdStudentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
    {
        if (studentDto == null) return BadRequest("Student data is required");
        var updatedStudentDto = await _libraryService.UpdateStudentAsync(id, studentDto);
        return Ok(updatedStudentDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _libraryService.DeleteStudentAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var studentDto = await _libraryService.GetStudentByIdAsync(id);
        return studentDto != null ? Ok(studentDto) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _libraryService.GetAllStudentsAsync();
        return Ok(students);
    }
}