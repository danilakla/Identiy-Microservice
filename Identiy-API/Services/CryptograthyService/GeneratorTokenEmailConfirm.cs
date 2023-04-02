using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace Identiy_API.Services.CryptograthyService
{
    public class GeneratorTokenEmailConfirm : ICrypto
    {
        private readonly IDataProtector dataProtectionProvider;

        public GeneratorTokenEmailConfirm(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            this.dataProtectionProvider = dataProtectionProvider.CreateProtector(configuration["AppSettings:SecretKeyCrypto"]);
        }

        public string EncryptSecretString<T>(T data)
        {
            try
            {
                try
                {
                    var encryptData = dataProtectionProvider.Protect(JsonConvert.SerializeObject(data));
                    return encryptData.ToString();

                }
                catch (Exception)
                {

                    throw;
                }
                    
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public T DecryptSecretString<T>(string secretStirng)
        {
            try
            {
                var decryptData=dataProtectionProvider.Unprotect(secretStirng);
                var data = JsonConvert.DeserializeObject<T>(decryptData);
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
