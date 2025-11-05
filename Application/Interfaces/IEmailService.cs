namespace LibraryManagementSystem.Application.Interfaces;

public interface IEmailService
{
    Task SendBookIssueNotificationAsync(
        string toEmail,
        string recipientName,
        int bookId,
        string bookTitle,
        int studentId,
        string studentName,
        DateTime issueDate,
        DateTime dueDate
        );
}