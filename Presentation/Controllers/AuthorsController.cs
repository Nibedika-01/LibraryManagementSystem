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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
    {
        if (authorDto == null) return BadRequest("Author data is required");
        var updatedAuthorDto = await _libraryService.UpdateAuthorAsync(id, authorDto);
        return Ok(updatedAuthorDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _libraryService.DeleteAuthorAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var authorDto = await _libraryService.GetAuthorByIdAsync(id);
        return authorDto != null ? Ok(authorDto) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _libraryService.GetAllAuthorsAsync();
        return Ok(authors);
    }
}