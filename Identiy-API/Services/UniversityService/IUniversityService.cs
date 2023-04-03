using Identiy_API.Model;

namespace Identiy_API.Services.UniversityService
{
    public interface IUniversityService
    {
        Task<PayloadManagerDTO> InitUniversity(CreateManagerDTO createManagerDTO);
        Task<PayloadManagerDTO> GetManagerData(LoginDTO loginDTO);
    }
}
