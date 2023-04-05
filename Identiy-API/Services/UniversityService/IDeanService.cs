using Identiy_API.Model.GrpcModel;
using Identiy_API.Model;

namespace Identiy_API.Services.UniversityService
{
    public interface IDeanService
    {
        Task<DeanPayload> GetDeanData(LoginDTO loginDTO);

        Task<DeanPayload> InitDean(DeanInitDTO deanInitDTO);
    }
}
