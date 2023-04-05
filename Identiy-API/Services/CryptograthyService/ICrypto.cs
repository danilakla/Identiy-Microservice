namespace Identiy_API.Services.CryptograthyService
{
    public interface ICrypto
    {
        
        string EncryptSecretString<T>(T data);
        T DecryptSecretString<T>(string secretStirng);


    }
}
