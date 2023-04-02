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
        public async Task RegistrationManager(CreateManagerDTO createManagerDTO)
        {
            try
            {
                var resu = crypto.DecryptSecretString<CreateManagerDTO>("CfDJ8MGeCBcQHChAowWjUXVynooVQ0hLXZ0V_mUXuGKo3Ibz7ltMSwMjVo1HUvcVaOyv0aBH0gLhloqapjQaNlFKwQqyqk4neN-M6bh5p_c0ci5qlNFs1YShM1ellTw9jmGjQ1req3elJO-Z1LJEvzTr66uiPR4IOe6hDj1l9qsxgB9LFKHommSw9XAp46HkdVw4xg-7pQhmqFmHtX15yBrRKQ_Dv5sW1KyU0OR3fatq7FwJjYI-tGtcipT6nucWo38EU9_jmlotsIl80K1abgm32GSbPJHEl51wg2PXPBLamkeF9zeOoGbN8qFzf3wFwBIwRw");
                var secretKey=crypto.EncryptSecretString(createManagerDTO);
            
                await tempSaveDataService.SaveManger(createManagerDTO,secretKey);
                var emailMessage = new EmailDto { To = createManagerDTO.Email, Subject = "Confirm email", Body = mailMessage.BodyHtml(secretKey) };
                mailService.SendEmail(emailMessage);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
