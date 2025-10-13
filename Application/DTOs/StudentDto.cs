namespace LibraryManagementSystem.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string ContactNo { get; set; }
    public required string Faculty { get; set; }
    public required string Semester { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateStudentDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string ContactNo { get; set; }
    public required string Faculty { get; set; }
    public required string Semester { get; set; }
}

public class UpdateStudentDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string ContactNo { get; set; }
    public required string Faculty { get; set; }
    public required string Semester { get; set; }
}

