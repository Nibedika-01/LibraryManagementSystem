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
        var createdIssueDto = await _libraryService.CreateIssueAsync(issueDto);
        return CreatedAtAction(nameof(GetIssueById), new { id = createdIssueDto.Id }, createdIssueDto);
    }`

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
}