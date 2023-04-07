using Identiy_API.Model.GrpcModel;
using Identiy_API.Model;

namespace Identiy_API.Services.UniversityService
{
    public interface IStudentService
    {
        Task<StudentPayload> GetStudentData(string email);

        Task<StudentPayload> InitStudent(StudentInitDto  studentInitDto);
    }
}
