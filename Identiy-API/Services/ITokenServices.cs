using Identiy_API.Model;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services
{
    public interface ITokenServices
    {
        string GetAccessTokenManager(CreateManagerDTO payload,string Role);
        string GetRefreshTokenManager(CreateManagerDTO payload);
    }
}
