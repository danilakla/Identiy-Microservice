using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
using Identiy_API.Model.Payload;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services
{
    public interface ITokenServices
    {
        string GetAccessTokenManager(ManagerPayload payload, string email);
        string GetRefreshTokenManager(ManagerPayload payload, string email);

        string GetAccessTokenDean(DeanPayload payload, string email);
        string GetRefreshTokenDean(DeanPayload payload, string email);

        string GetAccessTokenTeacher(TeacherPayload payload, string email);
        string GetRefreshTokenTeacher(TeacherPayload payload, string email);

        string GetAccessTokenStudent(StudentPayload payload, string email);
        string GetRefreshTokenStudent(StudentPayload payload, string email);
    }
}
