using Identiy_API.Model;
using Identiy_API.Model.Payload;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<IdentityUser> Login(LoginDTO createManagerDTO);
        Task SetRefreshToken(IdentityUser user, string refreshToken);
    }
}
