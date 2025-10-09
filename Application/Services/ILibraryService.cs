using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services;
public interface ILibraryService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(int id);

    Task<Author> CreateAuthorAsync(Author author);
    Task<IEnumerable<Author>> GetAllAuthorsAsync();

    Task<Book> CreateBookAsync (Book book);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task IssueBookAsync(int bookId, int studentId);

    Task<Student> CreateStudentAsync(Student student);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    
    Task<IEnumerable<Issue>> GetAllIssuesAsync();

    Task DeleteEntityAsync<T>(int id) where T : BaseEntity;
}
