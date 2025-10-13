namespace LibraryManagementSystem.Application.DTOs;

public class IssueDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateIssueDto
{
    public int BookId { get; set; }
    public int StudentId { get; set; }
    public DateTime? IssueDate { get; set; }
}

public class UpdateIssueDto
{
    public int BookId { get; set; }
    public int StudentId { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}