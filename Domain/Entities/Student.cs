namespace LibraryManagementSystem.Domain.Entities;
public class Student : BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
    public int ContactNo { get; set; }
	public string Faculty {  get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public ICollection<Issue> Issues { get; set; }
}
