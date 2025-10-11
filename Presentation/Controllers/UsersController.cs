using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public UsersController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        if (userDto == null) return BadRequest("User data is required");
        var createdUserDto = await _libraryService.CreateUserAsync(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUserDto.Id }, createdUserDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var userDto = await _libraryService.GetUserByIdAsync(id);
        return userDto != null ? Ok(userDto) : NotFound();
    }
}