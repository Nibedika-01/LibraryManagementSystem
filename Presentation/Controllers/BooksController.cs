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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto bookDto)
    {
        if (bookDto == null) return BadRequest("Book data is required");
        var updatedBookDto = await _libraryService.UpdateBookAsync(id, bookDto);
        return Ok(updatedBookDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _libraryService.DeleteBookAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var bookDto = await _libraryService.GetBookByIdAsync(id);
        return bookDto != null ? Ok(bookDto) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _libraryService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalBooks()
    {
        var total = await _libraryService.GetTotalBooksAsync();
        return Ok(new { total });
    }

    //[HttpPost("issue")]
    //public async Task<IActionResult> IssueBook([FromBody] IssueRequestDto request)
    //{
    //    var createdIssueDto = await _libraryService.IssueBookAsync(request.BookTitle!, request.StudentName!);
    //    return Ok(createdIssueDto);
    //}
}