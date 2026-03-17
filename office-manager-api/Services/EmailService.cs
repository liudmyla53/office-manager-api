namespace office_manager_api.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync (string to, string subject, string body)
        {
            // Implémentation de l'envoi d'email
            // Vous pouvez utiliser des bibliothèques comme SmtpClient, MailKit, etc.
            // Voici un exemple simple
            Console.WriteLine("========== EMAIL SENT ==========");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("================================");
            return Task.CompletedTask;
        }
    }
}
