using Identiy_API.DTO;
using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;

namespace Identiy_API.Services.UniversityService
{
    public interface IUniversityService
    {
        Task<PayloadManagerDTO> InitUniversity(CreateManagerDTO createManagerDTO);
        Task<PayloadManagerDTO> GetManagerData(LoginDTO loginDTO);


    }
}
