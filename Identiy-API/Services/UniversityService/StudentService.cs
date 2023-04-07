using Identiy_API.Model.GrpcModel;
using UniversityApi.Protos;

namespace Identiy_API.Services.UniversityService
{
    public class StudentService : IStudentService
    {
        private readonly Student.StudentClient studentClient;

        public StudentService(Student.StudentClient studentClient)
        {
            this.studentClient = studentClient;
        }
        public async Task<StudentPayload> GetStudentData(string email)
        {
            try
            {
                var response = await studentClient.GetStudentInfoAsync(new EmailStudent { Email = email });
                var responseData = new StudentPayload
                {
                    GroupId = response.GroupId,
                    FacultieId = response.FacultieId,
                    ProfessionId = response.ProfessionId,
                    StudentId = response.StudentId,
                    UniversityId = response.UniversityId,
                };
                return responseData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<StudentPayload> InitStudent(StudentInitDto studentInitDto)
        {
            try
            {

                var resData = new StudentRequest
                {
                    Email = studentInitDto.loginDTO.Email,
                    LastName = studentInitDto.loginDTO.LastName,
                    Name = studentInitDto.loginDTO.Name,
                    StudentIds = new StudentIds { GroupId = studentInitDto.GroupId }
                };
                var response = await studentClient.InitStudentAsync(resData);
                var responseData = new StudentPayload
                {
                    GroupId = response.GroupId,
                    FacultieId = response.FacultieId,
                    ProfessionId = response.ProfessionId,
                    StudentId = response.StudentId,
                    UniversityId = response.UniversityId,
                };
                return responseData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
