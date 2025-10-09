using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Application.Interfaces;

namespace LibraryManagementSystem.Application.Services;

public class LibraryService : ILibraryService
{
	private readonly IRepository<User> _userRepo;
	private readonly IRepository<Author> _authorRepo;
    private readonly IRepository<Book> _bookRepo;
    private readonly IRepository<Student> _studentRepo;
    private readonly IRepository<Issue> _issueRepo;

    public LibraryService(
        IRepository<User> userRepo,
        IRepository<Author> authorRepo,
        IRepository<Book> bookRepo,
        IRepository<Student> studentRepo,
        IRepository<Issue> issueRepo)
    {
        _userRepo = userRepo;
        _authorRepo = authorRepo;
        _bookRepo = bookRepo;
        _studentRepo = studentRepo;
        _issueRepo = issueRepo;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userRepo.AddAsync(user);
        return user;
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepo.GetByIdAsync(id);
    }
    
    public async Task<Author> CreateAuthorAsync(Author author)
    {
        await _authorRepo.AddAsync(author);
        return author;
    }
    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _authorRepo.GetAllAsync(a => !a.IsDeleted);
    }
    
    public async Task<Book> CreateBookAsync(Book book)
    {
        await _bookRepo.AddAsync(book);
        return book;
    }
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepo.GetAllAsync(b => !b.IsDeleted);
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        await _studentRepo.AddAsync(student);
        return student;
    }
    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepo.GetAllAsync(s => !s.IsDeleted);
    }

    public async Task<IEnumerable<Issue>> GetAllIssuesAsync()
    {
        return await _issueRepo.GetAllAsync(i => !i.IsDeleted);
    }

    public async Task IssueBookAsync (int bookId, int studentId)
    {
        var book = await _bookRepo.GetByIdAsync(bookId);
        if (book == null || book.IsDeleted) throw new Exception("Book not found");

        var student = await _studentRepo.GetByIdAsync (studentId);
        if (student == null || student.IsDeleted) throw new Exception("Student not found");

        var issue = new Issue {  BookId = bookId, StudentId = studentId };
        await _issueRepo.AddAsync(issue);
    }

    public async Task DeleteEntityAsync<T>(int id) where T : BaseEntity
    {
        var repo = GetRepository<T>();
        var entity = await repo.GetByIdAsync(id);
        if(entity != null)
        {
            entity.IsDeleted = true;
            await repo.UpdateAsync(entity);
        }
    }

    private IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        if (typeof(T) == typeof(User)) return (IRepository<T>) _userRepo;
        if (typeof(T) == typeof(Author)) return (IRepository<T>) _authorRepo;
        if (typeof(T) == typeof(Book)) return (IRepository<T>)_bookRepo;
        if (typeof(T) == typeof(Student)) return (IRepository<T>)_studentRepo;
        if (typeof(T) == typeof(Issue)) return (IRepository<T>)_issueRepo;
        throw new NotSupportedException();
    }
}
