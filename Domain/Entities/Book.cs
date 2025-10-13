namespace LibraryManagementSystem.Domain.Entities;

public class Book : BaseEntity
{
	public required string Title { get; set; }
	public int AuthorId { get; set; }
	public Author Author { get; set; } = null!;
	public required string Genre { get; set; }
	public required string Publisher { get; set; }
	public required DateTime? PublicationDate { get; set; }
	public ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
