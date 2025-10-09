namespace LibraryManagementSystem.Domain.Entities;

public class Author : BaseEntity
{
	public string AuthorName { get; set; } = string.Empty;
	public ICollection<Book> Books { get; set; }
}
