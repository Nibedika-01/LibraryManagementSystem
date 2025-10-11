namespace LibraryManagementSystem.Application.DTOs;

public class IssueDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int StudentId { get; set; }
    public DateTime IssueDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateIssueDto
{
    public int BookId { get; set; }
    public int StudentId { get; set; }
    public DateTime? IssueDate { get; set; }
}