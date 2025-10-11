namespace LibraryManagementSystem.Domain.Entities;

public class User : BaseEntity
{
	public required string Username { get; set; }
	public required string Password { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
