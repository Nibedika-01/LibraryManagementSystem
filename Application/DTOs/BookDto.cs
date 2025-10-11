namespace LibraryManagementSystem.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public bool isDeleted { get; set; }
}

public class CreateBookDto
{
    public required string Title { get; set; }
    public int AuthorId { get; set; }
}
