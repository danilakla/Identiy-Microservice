using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
using Org.BouncyCastle.Ocsp;
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
            try
            {
                var response = await deanClient.GetDeanInfoAsync(new EmailDean { Email = loginDTO.Email });
                
                return new DeanPayload {  DeanId= response.DeanId,  FacultieId=response.FacultieId, UniversityId=response.UniversityId  };
            }
            catch (Exception)
            {

                throw;
            }
          
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
