using Identiy_API.Model;

namespace Identiy_API.Services.Caching
{
    public interface ITempSaveDataService
    {
        Task SaveManger(CreateManagerDTO createManagerDTO, string tokenConfirmEmail);

        Task<bool> FindUser(string tokenConfirmEmail);
        Task<CreateManagerDTO> GetManager(string tokenConfirmEmail);

    }
}
