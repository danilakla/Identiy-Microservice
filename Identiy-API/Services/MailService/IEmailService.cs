using Identiy_API.Model;

namespace Identiy_API.Services.MailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
