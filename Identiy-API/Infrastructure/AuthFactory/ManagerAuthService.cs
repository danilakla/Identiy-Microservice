using Identiy_API.Model;
using Identiy_API.Services.UniversityService;
using Identiy_API.Services;
using Identiy_API.Services.Authentication;
using Identiy_API.Model.Payload;

namespace Identiy_API.Infrastructure.AuthFactory
{
    public class ManagerAuthService : IAuthFactoy
    {
        private readonly ITokenServices tokenServices;
        private readonly IUniversityService universityService;

        public ManagerAuthService(
        ITokenServices tokenServices,
        IUniversityService universityService
            )
        {
            this.tokenServices = tokenServices;
            this.universityService = universityService;
        }
        public async Task<JwtTokens> AuthenticateUser(LoginDTO loginDTO)
        {
            try
            {
                var ManagerIds = await universityService.GetManagerData(loginDTO);
                var managerPayload = new ManagerPayload { payloadManagerDTO = ManagerIds, Role = "Manager" };
                var accessToken = tokenServices.GetAccessTokenManager(managerPayload);
                var refreshToken = tokenServices.GetRefreshTokenManager(managerPayload);
                return new JwtTokens { AccessToken= accessToken, RefreshToken = refreshToken };

            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }
}
