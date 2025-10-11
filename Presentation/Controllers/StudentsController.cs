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

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _libraryService.GetAllStudentsAsync();
        return Ok(students);
    }
}