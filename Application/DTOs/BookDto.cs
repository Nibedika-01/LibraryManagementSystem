namespace LibraryManagementSystem.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublicationDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateBookDto
{
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublicationDate { get; set; }
}

public class UpdateBookDto
{
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublicationDate { get; set; }

}
