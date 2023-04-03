using Identiy_API.Model;
using Identiy_API.Model.Payload;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services
{
    public interface ITokenServices
    {
        string GetAccessTokenManager(ManagerPayload payload);
        string GetRefreshTokenManager(ManagerPayload payload);
    }
}
