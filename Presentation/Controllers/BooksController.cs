using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public BooksController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
    {
        if (bookDto == null) return BadRequest("Book data is required");
        var createdBookDto = await _libraryService.CreateBookAsync(bookDto);
        return Ok(createdBookDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _libraryService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpPost("{bookId}/issue/{studentId}")]
    public async Task<IActionResult> IssueBook(int bookId, int studentId)
    {
        var issueDto = new CreateIssueDto { BookId = bookId, StudentId = studentId };
        var createdIssueDto = await _libraryService.IssueBookAsync(issueDto);
        return Ok(createdIssueDto);
    }
}