using Identiy_API.Model;
using Identiy_API.Services;
using Identiy_API.Services.UniversityService;

namespace Identiy_API.Infrastructure.AuthFactory
{
    public class StudentAuthService : IAuthFactoy

    {
        private readonly ITokenServices tokenServices;
        private readonly IStudentService studentService;

        public StudentAuthService(ITokenServices tokenServices,
            IStudentService studentService)
        {
            this.tokenServices = tokenServices;
            this.studentService = studentService;
        }

        public async Task<JwtTokens> AuthenticateUser(LoginDTO loginDTO)
        {
            var ManagerIds = await studentService.GetStudentData(loginDTO.Email);
            var accessToken = tokenServices.GetAccessTokenStudent(ManagerIds);
            var refreshToken = tokenServices.GetRefreshTokenStudent(ManagerIds);
            return new JwtTokens { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
