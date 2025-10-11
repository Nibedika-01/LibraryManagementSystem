namespace LibraryManagementSystem.Domain.Entities;
public class Student : BaseEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; } 
    public required string ContactNo { get; set; }
	public required string Faculty {  get; set; } 
    public required string Semester { get; set; }
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
