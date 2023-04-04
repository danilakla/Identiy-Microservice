using Identiy_API.Model;

namespace Identiy_API.Services.Caching
{
    public interface ITempSaveDataService
    {
        Task SaveData<T>(T createManagerDTO, string tokenConfirmEmail);

        Task<T> GetData<T>(string tokenConfirmEmail);

    }
}
