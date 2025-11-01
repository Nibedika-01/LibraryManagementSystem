using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IssuesController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public IssuesController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto issueDto)
    {
        if (issueDto == null) return BadRequest("Issue data is required");

        var book = await _libraryService.GetBookByIdAsync(issueDto.BookId);
        if (book == null) return BadRequest("Book not found");

        if (!book.IsAvailable) return BadRequest("Book is already issued");

        var createdIssueDto = await _libraryService.CreateIssueAsync(issueDto);

        var updateBookDto = new UpdateBookDto
        {
            Title = book.Title,
            IsAvailable = false
        };
        await _libraryService.UpdateBookAsync(book.Id, updateBookDto);
        return CreatedAtAction(nameof(GetIssueById), new { id = createdIssueDto.Id }, createdIssueDto);
    }

    [HttpPost("return/{id}")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var issue = await _libraryService.GetIssueByIdAsync(id);
        if (issue == null) return NotFound("Issue record not found");
        if (issue.ReturnDate != null) return BadRequest("Book is already returned");

        var updateIssueDto = new UpdateIssueDto
        {
            BookId = issue.BookId,
            StudentId = issue.StudentId,
            IssueDate = issue.IssueDate,
            ReturnDate = DateTime.UtcNow  // Use UtcNow for consistency
        };

        await _libraryService.UpdateIssueAsync(id, updateIssueDto);

        var book = await _libraryService.GetBookByIdAsync(issue.BookId);
        if (book != null)
        {
            var updateBookDto = new UpdateBookDto
            {
                Title = book.Title,
                AuthorId = book.AuthorId,  // Add this
                Genre = book.Genre,        // Add this
                Publisher = book.Publisher, // Add this
                PublicationDate = book.PublicationDate, // Add this
                IsAvailable = true
            };
            await _libraryService.UpdateBookAsync(book.Id, updateBookDto);
        }

        return Ok(new { message = "Book returned successfully", issue = updateIssueDto });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIssue(int id, [FromBody] UpdateIssueDto issueDto)
    {
        if (issueDto == null) return BadRequest("Issue data is required");
        var updatedIssueDto = await _libraryService.UpdateIssueAsync(id, issueDto);
        return Ok(updatedIssueDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIssue(int id)
    {
        await _libraryService.DeleteIssueAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIssueById(int id)
    {
        var issueDto = await _libraryService.GetIssueByIdAsync(id);
        return issueDto != null ? Ok(issueDto) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIssues()
    {
        var issues = await _libraryService.GetAllIssuesAsync();
        return Ok(issues);
    }

    [HttpGet("total")]
    public async Task<IActionResult> GetTotalIssues()
    {
        var total = await _libraryService.GetTotalIssuesAsync();
        return Ok(new { total });
    }
}