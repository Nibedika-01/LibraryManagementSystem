namespace LibraryManagementSystem.Domain.Entities;

public class User : BaseEntity
{
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
