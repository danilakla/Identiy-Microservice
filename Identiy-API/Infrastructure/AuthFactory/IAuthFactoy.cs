using Identiy_API.Model;

namespace Identiy_API.Infrastructure.AuthFactory
{
    public interface IAuthFactoy
    {

        Task<JwtTokens >AuthenticateUser(LoginDTO loginDTO);
    }
}
