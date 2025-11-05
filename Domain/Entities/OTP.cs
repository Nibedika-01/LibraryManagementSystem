namespace LibraryManagementSystem.Domain.Entities;
public class OTP : BaseEntity
{
	public string Email { get; set; } = string.Empty;
	public string OtpCode { get; set; } = string.Empty;
	public DateTime ExpiryTime { get; set; }
	public bool IsUsed { get; set; } = false;
	public int Attempts { get; set; } = 0;
	public string Purpose { get; set; } = "LOGIN";
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
