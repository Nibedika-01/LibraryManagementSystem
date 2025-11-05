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

    public async Task SendBookIssueNotificationAsync(
        string toEmail,
        string recipientName,
        int bookId,
        string bookTitle,
        int studentId,
        string studentName,
        DateTime issueDate,
        DateTime dueDate
        )
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
        message.To.Add(new MailboxAddress(recipientName, toEmail));
        message.Subject = "Book Issued - Library Management System";

        var htmlBody = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;'>
                    <div style='background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
                        <h2 style='color: #007bff; margin-bottom: 20px;'>📚 Book Issue Notification</h2>
                        
                        <p style='color: #333; font-size: 16px;'>Dear <strong>{recipientName}</strong>,</p>
                        
                        <p style='color: #666; font-size: 14px;'>A book has been successfully issued. Here are the details:</p>
                        
                        <div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0;'>
                            <table style='width: 100%; border-collapse: collapse;'>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Book ID:</td>
                                    <td style='padding: 8px; color: #333;'>{bookId}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Book Title:</td>
                                    <td style='padding: 8px; color: #333;'>{bookTitle}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Student ID:</td>
                                    <td style='padding: 8px; color: #333;'>{studentId}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Student Name:</td>
                                    <td style='padding: 8px; color: #333;'>{studentName}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Issue Date:</td>
                                    <td style='padding: 8px; color: #333;'>{issueDate:MMMM dd, yyyy}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px; color: #666; font-weight: bold;'>Due Date:</td>
                                    <td style='padding: 8px; color: #28a745; font-weight: bold;'>{dueDate:MMMM dd, yyyy}</td>
                                </tr>
                            </table>
                        </div>
                        
                        <div style='background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0;'>
                            <p style='color: #856404; margin: 0; font-size: 14px;'>
                                ⚠️ <strong>Important:</strong> Please return the book on or before the due date to avoid late fees.
                            </p>
                        </div>
                        
                        <p style='color: #666; font-size: 14px; margin-top: 20px;'>Thank you for using our library!</p>
                        
                        <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                        <p style='color: #999; font-size: 12px;'>This is an automated message from Library Management System. Please do not reply to this email.</p>
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

            Console.WriteLine($"Book issue notification sent to: {toEmail}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email to {toEmail}: {ex.Message}");
            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }
}