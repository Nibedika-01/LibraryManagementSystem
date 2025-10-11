namespace LibraryManagementSystem.Domain.Entities;

public class Author : BaseEntity
{
	public required string AuthorName { get; set; }
	public ICollection<Book> Books { get; set; } = new List<Book>();
}
