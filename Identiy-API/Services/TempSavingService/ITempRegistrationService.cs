using Identiy_API.Model;

namespace Identiy_API.Services.TempSavingService
{
    public interface ITempRegistrationService
    {
        Task RegistrationManager(CreateManagerDTO createManagerDTO);
    }
}
