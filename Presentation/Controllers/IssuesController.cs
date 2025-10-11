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

    [HttpGet]
    public async Task<IActionResult> GetAllIssues()
    {
        var issues = await _libraryService.GetAllIssuesAsync();
        return Ok(issues);
    }
}