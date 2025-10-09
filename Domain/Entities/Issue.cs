namespace LibraryManagementSystem.Domain.Entities;
public class Issue : BaseEntity
{
	public int BookId { get; set; }
	public Book Book { get; set; }
	public int StudentId {  get; set; }
	public Student Student { get; set; }
	public DateTime IssueDate { get; set; } = DateTime.UtcNow;
}
