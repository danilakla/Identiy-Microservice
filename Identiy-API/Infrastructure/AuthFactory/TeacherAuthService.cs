using Identiy_API.Model;
using Identiy_API.Services;
using Identiy_API.Services.UniversityService;

namespace Identiy_API.Infrastructure.AuthFactory
{
    public class TeacherAuthService : IAuthFactoy
    {
        private readonly ITokenServices tokenServices;
        private readonly IUniversityService universityService;

        public TeacherAuthService(ITokenServices tokenServices,
            IUniversityService universityService)
        {
            this.tokenServices = tokenServices;
            this.universityService = universityService;
        }


        public async Task<JwtTokens> AuthenticateUser(LoginDTO loginDTO)
        {
            var ManagerIds = await universityService.GetTeacherData(loginDTO.Email);
            var accessToken = tokenServices.GetAccessTokenTeacher(ManagerIds);
            var refreshToken = tokenServices.GetRefreshTokenTeacher(ManagerIds);
        return new JwtTokens { AccessToken= accessToken, RefreshToken = refreshToken };
        }
    }
}
