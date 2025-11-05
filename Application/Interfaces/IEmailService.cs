namespace LibraryManagementSystem.Application.Interfaces;

public interface IEmailService
{
    Task SendOtpEmailAsync(string toEmail, string otpCode);
    Task SendRegistrationOtpEmailAsync(string toEmail, string otpCode);
}