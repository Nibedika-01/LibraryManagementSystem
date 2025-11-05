namespace LibraryManagementSystem.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? OtpCode { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class LoginDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class UpdateUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class SendOtpRequestDto
{
    public string Username { get; set; } = string.Empty;
}
public class VerifyOtpDto
{
    public required string Username { get; set; }
    public required string OtpCode { get; set; }
    public required string Password { get; set; }
}

public class OtpResponseDto 
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime? ExpiryTime { get; set; }
}

