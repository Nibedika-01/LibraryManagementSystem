namespace LibraryManagementSystem.Domain.Entities;
public class Issue : BaseEntity
{
	public int BookId { get; set; }
	public Book Book { get; set; } = null!;
	public int StudentId {  get; set; }
	public Student Student { get; set; } = null!;
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
}
