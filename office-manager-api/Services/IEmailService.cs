namespace office_manager_api.Services
{
    public interface IEmailService
    {
        Task  SendEmailAsync (string toEmail, string subject, string body);
    }
}
