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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (loginDto == null) return BadRequest("Login data is required");
        try
        {
            var user = await _libraryService.LoginAsync(loginDto.Username, loginDto.Password);
            return Ok(user);
        } catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        if (userDto == null) return BadRequest("User data is required");
        var updatedUserDto = await _libraryService.UpdateUserAsync(id, userDto);
            return Ok(updatedUserDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _libraryService.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var userDto = await _libraryService.GetUserByIdAsync(id);
        return userDto != null ? Ok(userDto) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _libraryService.GetAllUsersAsync();
        return Ok(users);
    }
}