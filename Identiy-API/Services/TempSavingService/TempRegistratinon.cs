using Identiy_API.Model;
using Identiy_API.Services.Caching;
using Identiy_API.Services.CryptograthyService;
using Identiy_API.Services.MailService;
using Identiy_API.Utils;
using MailKit;
using Newtonsoft.Json;
using System.Net.Mail;

namespace Identiy_API.Services.TempSavingService
{
    public class TempRegistratinon : ITempRegistrationService
    {
        private readonly ICrypto crypto;
        private readonly ITempSaveDataService tempSaveDataService;
        private readonly IEmailService mailService;
        private readonly EmailMessages mailMessage;

        public TempRegistratinon(ICrypto crypto,ITempSaveDataService tempSaveDataService, IEmailService mailService ,EmailMessages mailMessage)
        {
            this.crypto = crypto;
            this.tempSaveDataService = tempSaveDataService;
            this.mailService = mailService;
            this.mailMessage = mailMessage;
        }
        public async Task TempRegistration<T>(T UserDto,string role) where T: LoginDTO
        {
            try
            {
                var secretKey=crypto.EncryptSecretString(UserDto);
            bool flag= typeof(T)==typeof(CreateManagerDTO)?true:false;
                await tempSaveDataService.SaveData(UserDto, secretKey);
                var emailMessage = new EmailDto { To = UserDto.Email, Subject = "Confirm email", Body = mailMessage.BodyHtml(secretKey, role) };
                mailService.SendEmail(emailMessage);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
