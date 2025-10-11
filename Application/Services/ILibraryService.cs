using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services;
public interface ILibraryService
{
    Task<UserDto> CreateUserAsync(CreateUserDto userDto);
    Task<UserDto> GetUserByIdAsync(int id);

    Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto);
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

    Task<BookDto> CreateBookAsync (CreateBookDto bookDto);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task <IssueDto> IssueBookAsync(CreateIssueDto issueDto);

    Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto);
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    
    Task<IEnumerable<IssueDto>> GetAllIssuesAsync();

    Task DeleteEntityAsync<T>(int id) where T : BaseEntity;
}
