using Identiy_API.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Identiy_API.Services.MailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void SendEmail(EmailDto request)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["AppSettings:AppEmail"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration["AppSettings:EmailHost"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration["AppSettings:AppEmail"], configuration["AppSettings:PasswordEmail"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
