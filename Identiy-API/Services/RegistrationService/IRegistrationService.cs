using Identiy_API.Model;

namespace Identiy_API.Services.RegistrationService
{
    public interface IRegistrationService
    {
        Task RegistrationManager(CreateManagerDTO createManagerDTO);
        Task<bool> IsRegistration(string email);
    }
}
