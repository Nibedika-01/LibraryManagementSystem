using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using BCrypt.Net;

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

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Password =  BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.CreatedAt = user.CreatedAt != default ? user.CreatedAt : DateTime.UtcNow;
        await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _authorRepository.AddAsync(author);
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync(a => !a.IsDeleted);
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _bookRepository.AddAsync(book);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync(b => !b.IsDeleted);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto);
        await _studentRepository.AddAsync(student);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync(s => !s.IsDeleted);
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<IssueDto> IssueBookAsync(CreateIssueDto issueDto)
    {
        var book = await _bookRepository.GetByIdAsync(issueDto.BookId);
        if (book == null || book.IsDeleted) throw new Exception("Book not found");

        var student = await _studentRepository.GetByIdAsync(issueDto.StudentId);
        if (student == null || student.IsDeleted) throw new Exception("Student not found");

        var issue = _mapper.Map<Issue>(issueDto);
        issue.IssueDate = issue.IssueDate != default ? issue.IssueDate : DateTime.UtcNow;
        await _issueRepository.AddAsync(issue);
        return _mapper.Map<IssueDto>(issue);
    }

    public async Task<IEnumerable<IssueDto>> GetAllIssuesAsync()
    {
        var issues = await _issueRepository.GetAllAsync(i => !i.IsDeleted);
        return _mapper.Map<IEnumerable<IssueDto>>(issues);
    }

    public async Task DeleteEntityAsync<T>(int id) where T : BaseEntity
    {
        var repository = _repositoryFactory.GetRepository<T>();
        await repository.DeleteAsync(id);
    }
}