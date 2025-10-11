using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public AuthorsController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
    {
        if (authorDto == null) return BadRequest("Author data is required");
        var createdAuthorDto = await _libraryService.CreateAuthorAsync(authorDto);
        return Ok(createdAuthorDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _libraryService.GetAllAuthorsAsync();
        return Ok(authors);
    }
}