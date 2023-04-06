using Identiy_API.Model;

namespace Identiy_API.Services.TempSavingService
{
    public interface ITempRegistrationService
    {
        Task TempRegistration<T>(T createManagerDTO, string role) where T : LoginDTO;
    }
}
