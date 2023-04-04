using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
using UniversityApi.Protos;

namespace Identiy_API.Services.UniversityService
{
    public class DeanService : IDeanService
    {
        private readonly Dean.DeanClient deanClient;

        public DeanService(Dean.DeanClient deanClient)
        {
            this.deanClient = deanClient;
        }
        public async Task<DeanPayload> GetDeanData(LoginDTO loginDTO)
        {
                throw new NotImplementedException();

        }

        public async Task<DeanPayload> InitDean(DeanInitDTO deanInitDTO)
        {
            var respons = await deanClient.InitDeanAsync(MapToRequserDean(deanInitDTO));

            return new DeanPayload { DeanId = respons.DeanId };
        }

        private DeanRequest MapToRequserDean(DeanInitDTO  deanInitDTO) {

            return new DeanRequest { DeansId = new DeanIds { 
                FacultieId=deanInitDTO.DeanTokenRegistraion.FacultieId,
            UniversityId=deanInitDTO.DeanTokenRegistraion.FacultieId}, 
            Email=deanInitDTO.loginDTO.Email,
            Name=deanInitDTO.loginDTO.Name,
            LastName=deanInitDTO.loginDTO.LastName,
            };
        }
    }
}
