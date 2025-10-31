using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Query;

namespace LibraryManagementSystem.Application.Services;

public class LibraryService : ILibraryService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Issue> _issueRepository;
    private readonly IRepositoryFactory _repositoryFactory;
    private readonly IMapper _mapper;

    public LibraryService(
        IRepository<User> userRepository,
        IRepository<Author> authorRepository,
        IRepository<Book> bookRepository,
        IRepository<Student> studentRepository,
        IRepository<Issue> issueRepository,
        IRepositoryFactory repositoryFactory,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _studentRepository = studentRepository;
        _issueRepository = issueRepository;
        _repositoryFactory = repositoryFactory;
        _mapper = mapper;
    }

    //User
    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Password =  BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.CreatedAt = user.CreatedAt != default ? user.CreatedAt : DateTime.UtcNow;
        await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> LoginAsync(string username, string password)
    {
        var user = (await _userRepository.GetAllAsync(u => u.Username.ToLower() == username.ToLower())).FirstOrDefault();

        if(user == null)
        {
            throw new Exception("User not found");
        }

        bool isPasswordField = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordField)
            throw new Exception("Password do not match");
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) throw new Exception("User not found");

        user.Username = userDto.Username ?? user.Username;
        if (!string.IsNullOrEmpty(userDto.Password)) user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.CreatedAt = userDto.CreatedAt ?? user.CreatedAt;

        await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync(a => !a.IsDeleted);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    //Author
    public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _authorRepository.AddAsync(author);
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<AuthorDto> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null) throw new Exception("Author not found");

        author.AuthorName = authorDto.AuthorName ?? author.AuthorName;

        await _authorRepository.UpdateAsync(author);
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        await _authorRepository.DeleteAsync(id);
    }

    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync(a => !a.IsDeleted);
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    //Book
    public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _bookRepository.AddAsync(book);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> UpdateBookAsync(int id, UpdateBookDto bookDto)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) throw new Exception("Book not found");

        book.Title = bookDto.Title ?? book.Title;
        book.AuthorId = bookDto.AuthorId != 0 ? bookDto.AuthorId : book.AuthorId;
        book.Genre = bookDto.Genre ?? book.Genre;
        book.Publisher = bookDto.Publisher ?? book.Publisher;
        book.PublicationDate = bookDto.PublicationDate ?? book.PublicationDate;

        await _bookRepository.UpdateAsync(book);
        return _mapper.Map<BookDto>(book);

    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<int> GetTotalBooksAsync()
    {
       return await _bookRepository.GetTotalCountAsync();
    }

    //Student
    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        await _studentRepository.AddAsync(student);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto studentDto)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null) throw new Exception("Student not found");

        student.Name = studentDto.Name ?? student.Name;
        student.Address = studentDto.Address ?? student.Address;
        student.ContactNo = studentDto.ContactNo ?? student.ContactNo;
        student.Faculty = studentDto.Faculty ?? student.Faculty;
        student.Semester = studentDto.Semester ?? student.Semester;

        await _studentRepository.UpdateAsync(student);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
        await _studentRepository.DeleteAsync(id);
    }

    public async Task<StudentDto> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }
    public async Task<int> GetTotalStudentsAsync()
    {
        return await _studentRepository.GetTotalCountAsync();
    }


    //Issue
    public async Task<IssueDto> CreateIssueAsync(CreateIssueDto issueDto)
    {
        var book = await _bookRepository.GetByIdAsync(issueDto.BookId);
        if (book == null || book.IsDeleted) throw new Exception($"Book with id {issueDto.BookId} not found");

        var student = await _studentRepository.GetByIdAsync(issueDto.StudentId);
        if (student == null || student.IsDeleted) throw new Exception($"Student with id {issueDto.StudentId} not found");

        var existingIssues = await _issueRepository.GetAllAsync(i => !i.IsDeleted && i.BookId == issueDto.BookId && !i.ReturnDate.HasValue);
        if (existingIssues.Any()) throw new Exception("Book is already issued and not yet returned");

        var issue = new Issue
        {
            BookId = issueDto.BookId,
            StudentId = issueDto.StudentId,
            IssueDate = issueDto.IssueDate ?? DateTime.UtcNow
        };

        await _issueRepository.AddAsync(issue);

        var issueResult = _mapper.Map<IssueDto>(issue);
        issueResult.BookTitle = book.Title;
        issueResult.StudentName = student.Name;
        return issueResult;
    }

    public async Task<IssueDto> UpdateIssueAsync(int id, UpdateIssueDto issueDto)
    {
        var issue = await _issueRepository.GetByIdAsync(id);
        if (issue == null) throw new KeyNotFoundException($"Issue with id {id} not found");

        if (issueDto.BookId != 0)
        {
            var book = await _bookRepository.GetByIdAsync(issueDto.BookId);
            if (book == null || book.IsDeleted) throw new Exception($"Book with id {issueDto.BookId} not found");
            issue.BookId = issueDto.BookId;
        }

        if (issueDto.StudentId != 0)
        {
            var student = await _studentRepository.GetByIdAsync(issueDto.StudentId);
            if (student == null || student.IsDeleted) throw new Exception($"Student with id {issueDto.StudentId} not found");
            issue.StudentId = issueDto.StudentId;
        }

        if (issueDto.IssueDate.HasValue)
        {
            issue.IssueDate = issueDto.IssueDate.Value;
        }

        if (issueDto.ReturnDate.HasValue)
        {
            issue.ReturnDate = issueDto.ReturnDate.Value;
        }

        await _issueRepository.UpdateAsync(issue);

        var book2 = await _bookRepository.GetByIdAsync(issue.BookId);
        var student2 = await _studentRepository.GetByIdAsync(issue.StudentId);
        var result = _mapper.Map<IssueDto>(issue);
        result.BookTitle = book2?.Title;
        result.StudentName = student2?.Name;
        return result;
    }


    public async Task DeleteIssueAsync(int id)
    {
        await _issueRepository.DeleteAsync(id);
    }

    public async Task<IssueDto> GetIssueByIdAsync(int id)
    {
        var issue = await _issueRepository.GetByIdAsync(id);
        var issueDto = _mapper.Map<IssueDto>(issue);
        if (issue.Book != null) issueDto.BookTitle = issue.Book.Title;
        if (issue.Student != null) issueDto.StudentName = issue.Student.Name;
        return issueDto;
    }

    public async Task<IEnumerable<IssueDto>> GetAllIssuesAsync()
    {
        var issues= await _issueRepository.GetAllAsync(i => !i.IsDeleted);
        var issueDtos = _mapper.Map<IEnumerable<IssueDto>>(issues);
        foreach(var issue in issues)
        {
            if(issue.Book != null)
            {
                var issueDto = issueDtos.FirstOrDefault(d => d.Id == issue.Id);
                if(issueDto != null)
                {
                    issueDto.BookTitle = issue.Book.Title;
                    issueDto.StudentName = issue.Student?.Name;
                }
            }
        }
        return issueDtos;
    }

    public async Task<int> GetTotalIssuesAsync()
    {
        return await _issueRepository.GetTotalCountAsync();
    }

    //public async Task<IssueDto> IssueBookAsync(string bookTitle, string studentName)
    //{
    //    var books = await _bookRepository.GetAllAsync(b => !b.IsDeleted && b.Title == bookTitle &&
    //!b.Issues.Any(i => !i.IsDeleted));

    //    var book = books.FirstOrDefault();
    //    if (book == null) throw new Exception("Available book not found with the given title");

    //    var students = await _studentRepository.GetAllAsync(s => !s.IsDeleted && s.Name == studentName);
    //    var student = students.FirstOrDefault();
    //    if (student == null) throw new Exception("Student not found with the given name");

    //    var issue = new Issue
    //    {
    //        BookId = book.Id,
    //        StudentId = student.Id,
    //        IssueDate = DateTime.UtcNow
    //    };

    //    await _issueRepository.AddAsync(issue);
    //    issue.Book = book;
    //    issue.Student = student;
    //    return _mapper.Map<IssueDto>(issue);
    //}
}