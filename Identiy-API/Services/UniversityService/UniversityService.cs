using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
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
            catch (Exception e )
            {
                Console.WriteLine(e.Message);

                throw e;
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

    

        public async Task<TeacherPayload> InitTeacher(TeacherInitDto teacherInitDto)
        {

            try
            {
                var request = MapToRequestInitTeacher(teacherInitDto);
                var response = await universityClient.InitTeacherAsync(request);
                var  teacherPayload = MapResponseToTeacherPayload(response);

                return teacherPayload;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TeacherPayload> GetTeacherData(string email)
        {
            try
            {
                var request = new Email { Email_=email };
                var response = await universityClient.GetTeacherInfoAsync(request);
                var payload= MapResponseToTeacherPayload(response);
                return payload;
            }

            catch (Exception)
            {

                throw;
            }
        }
        private TeacherPayload MapResponseToTeacherPayload(TeacherResponse response)
        {
            var teacherPayload = new TeacherPayload { UniversityId = response.UniversityId, TeacherId = response.TeacherId };
return teacherPayload;
        }
        private TeacherRequest MapToRequestInitTeacher(TeacherInitDto teacherInitDto)
        {
            return new TeacherRequest
            {
                Email = teacherInitDto.loginDTO.Email,
                LastName = teacherInitDto.loginDTO.LastName,
                Name=teacherInitDto.loginDTO.Name,
                UniversityId=teacherInitDto.UniversityId,
            };
        }

  
    }
}
