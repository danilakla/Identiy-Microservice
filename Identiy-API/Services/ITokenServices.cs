using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
using Identiy_API.Model.Payload;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services
{
    public interface ITokenServices
    {
        string GetAccessTokenManager(ManagerPayload payload);
        string GetRefreshTokenManager(ManagerPayload payload);

        string GetAccessTokenDean(DeanPayload payload);
        string GetRefreshTokenDean(DeanPayload payload);

        string GetAccessTokenTeacher(TeacherPayload payload);
        string GetRefreshTokenTeacher(TeacherPayload payload);
    }
}
