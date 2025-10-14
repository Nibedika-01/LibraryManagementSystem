using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services;
public interface ILibraryService
{
    //User
    Task<UserDto> CreateUserAsync(CreateUserDto userDto);
    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto);
    Task DeleteUserAsync(int id);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();

    //Author
    Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto);
    Task<AuthorDto> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);
    Task DeleteAuthorAsync(int id);
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

    //Book
    Task<BookDto> CreateBookAsync(CreateBookDto bookDto);
    Task<BookDto> UpdateBookAsync(int id, UpdateBookDto bookDto);
    Task DeleteBookAsync(int id);
    Task<BookDto> GetBookByIdAsync(int id);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();

    //Student
    Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto);
    Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto studentDto);
    Task DeleteStudentAsync(int id);
    Task<StudentDto> GetStudentByIdAsync(int id);
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();

    //Issue
    Task<IssueDto> CreateIssueAsync(CreateIssueDto issueDto);
    Task<IssueDto> UpdateIssueAsync(int id, UpdateIssueDto issueDto);
    Task DeleteIssueAsync(int id);
    Task<IssueDto> GetIssueByIdAsync(int id);
    Task<IEnumerable<IssueDto>> GetAllIssuesAsync();

    //Task<IssueDto> IssueBookAsync(string bookTitle, string studentName);
}