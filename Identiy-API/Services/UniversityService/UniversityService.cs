using Identiy_API.Model;
using UniversityApi.Protos;
using University = UniversityApi.Protos.University;

namespace Identiy_API.Services.UniversityService
{
    public class UniversityService : IUniversityService
    {
        private readonly University.UniversityClient universityClient;

        public UniversityService(University.UniversityClient universityClient)
        {
            this.universityClient = universityClient;
        }
        public async Task<PayloadManagerDTO> InitUniversity(CreateManagerDTO createManagerDTO)
        {

            try
            {
                var request = MapToRequestInitUniversity(createManagerDTO);
                var response = await universityClient.InitUniversityAsync(request);
                var managerPayload = MapToResponsetManager(response);
                return managerPayload; 
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public async Task<PayloadManagerDTO> GetManagerData(LoginDTO  loginDTO)
        {

            try
            {
                var request = MapToRequestGetManagerData(loginDTO);
                var response = await universityClient.GetManagerInfoAsync(request);
                var managerPayload = MapToResponsetManager(response);
                return managerPayload;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private PayloadManagerDTO MapToResponsetManager(ManagerRespone managerRespone)
        {
            return new PayloadManagerDTO
            {
                ManagerId = managerRespone.ManagerId,
                UniversityId = managerRespone.UniversityId


            };
        }

        private Email MapToRequestGetManagerData(LoginDTO  loginDTO)
        {
            return new Email
            {
                Email_= loginDTO.Email,

            };
        }

        private ManagerRequest MapToRequestInitUniversity(CreateManagerDTO createManagerDTO )
        {
            return new ManagerRequest { 
                Name= createManagerDTO.Name,
                 LastName= createManagerDTO.LastName,
                 Email= createManagerDTO.Email,
                 University=new UniversityInfo { Name=createManagerDTO.University.Name,
                 Address=createManagerDTO.University.Address}
            
            };
        }
    }
}
