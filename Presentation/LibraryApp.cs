using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Presentation;

public class LibraryApp
{
    private readonly ILibraryService _libraryService;

    public LibraryApp(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Library Manegement System");

        var author = new Author { AuthorName = "Collen Hover" };
        await _libraryService.CreateAuthorAsync(author);
        Console.WriteLine($"Created Author: {author.AuthorName}");

        var book = new Book { Title = "It ends with us", AuthorId = author.Id };
        await _libraryService.CreateBookAsync(book);
        Console.WriteLine($"Created Book: {book.Title}");

        var student = new Student { Name = "Nibedika Gautam", Address = "Hetauda 5", ContactNo = 989898989, Faculty = "CS", Semester = "8" };
        await _libraryService.CreateStudentAsync(student);
        Console.WriteLine($"Created Student: {student.Name}");

        await _libraryService.IssueBookAsync(book.Id, student.Id);
        Console.WriteLine("Book Issued");

        var books = await _libraryService.GetAllBooksAsync();
        Console.WriteLine("All Books:");
        foreach (var b in books) Console.WriteLine(b.Title);

        await _libraryService.DeleteEntityAsync<Book>(book.Id);
        Console.WriteLine("Book Deleted");

        books = await _libraryService.GetAllBooksAsync();
        Console.WriteLine("Books after delete: " + books.Count());
    }
}