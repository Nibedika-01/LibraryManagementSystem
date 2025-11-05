using LibraryManagementSystem.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Existing method for login OTP
    public async Task SendOtpEmailAsync(string toEmail, string otpCode)
    {
        await SendEmailAsync(toEmail, otpCode, "Login");
    }

    // NEW method for registration OTP
    public async Task SendRegistrationOtpEmailAsync(string toEmail, string otpCode)
    {
        await SendEmailAsync(toEmail, otpCode, "Registration");
    }

    // Shared email sending logic
    private async Task SendEmailAsync(string toEmail, string otpCode, string purpose)
    {
        var fromEmail = _configuration["EmailSettings:FromEmail"]?.Trim();
        var smtpServer = _configuration["EmailSettings:SmtpServer"]?.Trim();
        var portString = _configuration["EmailSettings:Port"]?.Trim();
        var username = _configuration["EmailSettings:Username"]?.Trim();
        var password = _configuration["EmailSettings:Password"];

        if (string.IsNullOrWhiteSpace(fromEmail))
            throw new Exception("EmailSettings:FromEmail is missing");

        if (string.IsNullOrWhiteSpace(smtpServer))
            throw new Exception("EmailSettings:SmtpServer is missing");

        if (string.IsNullOrWhiteSpace(portString) || !int.TryParse(portString, out int port))
            throw new Exception("EmailSettings:Port is missing or invalid");

        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("EmailSettings:Username is missing");

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("EmailSettings:Password is missing");

        if (!toEmail.Contains("@") || !toEmail.Contains("."))
            throw new Exception($"Recipient email '{toEmail}' is not valid");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Library System", fromEmail));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = $"Your OTP Code - Library Management System ({purpose})";

        var htmlBody = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;'>
                    <div style='background-color: white; padding: 30px; border-radius: 10px;'>
                        <h2 style='color: #333;'>Library Management System</h2>
                        <p style='color: #666; font-size: 16px;'>Your OTP code for {purpose.ToLower()} is:</p>
                        <div style='background-color: #007bff; color: white; padding: 15px; text-align: center; font-size: 32px; font-weight: bold; letter-spacing: 5px; border-radius: 5px; margin: 20px 0;'>
                            {otpCode}
                        </div>
                        <p style='color: #666; font-size: 14px;'>This OTP will expire in <strong>5 minutes</strong>.</p>
                        <p style='color: #666; font-size: 14px;'>If you didn't request this code, please ignore this email.</p>
                        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='color: #999; font-size: 12px;'>This is an automated message, please do not reply.</p>
                    </div>
                </div>
            </body>
            </html>
        ";

        message.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }
}