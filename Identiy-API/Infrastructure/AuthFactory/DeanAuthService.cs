using Identiy_API.Model;
using Identiy_API.Services;
using Identiy_API.Services.UniversityService;

namespace Identiy_API.Infrastructure.AuthFactory
{
    public class DeanAuthService : IAuthFactoy
    {
        private readonly ITokenServices tokenServices;
        private readonly IDeanService deanService;

        public DeanAuthService(ITokenServices tokenServices,
            IDeanService deanService)
        {
            this.tokenServices = tokenServices;
            this.deanService = deanService;
        }
        public async Task<JwtTokens> AuthenticateUser(LoginDTO loginDTO)
        {
            var ManagerIds = await deanService.GetDeanData(loginDTO);
            var accessToken = tokenServices.GetRefreshTokenDean(ManagerIds, loginDTO.Email);
            var refreshToken = tokenServices.GetRefreshTokenDean(ManagerIds, loginDTO.Email);
                return new JwtTokens { AccessToken= accessToken, RefreshToken = refreshToken };
        }
    }
}
