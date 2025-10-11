namespace LibraryManagementSystem.Application.DTOs;

public class AuthorDto
{
    public int Id { get; set; }
    public required string AuthorName { get; set; } 
    public bool IsDeleted { get; set; }

}

public class CreateAuthorDto
{
    public required string AuthorName { get; set; }
}
