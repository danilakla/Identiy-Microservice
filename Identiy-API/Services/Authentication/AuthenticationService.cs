using Identiy_API.Model;
using Identiy_API.Model.Payload;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

    
        public async Task<IdentityUser> Login(LoginDTO createManagerDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(createManagerDTO.Email);
                if(user is null)
                {
                    throw new Exception("Account doesn't exist");
                }
                var isValidPassword=await userManager.CheckPasswordAsync(user, createManagerDTO.Password);
                if (!isValidPassword)
                {
                    throw new Exception("Password is not correct");

                }
                return user;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SetRefreshToken(IdentityUser user,string refreshToken)
        {


            await userManager.SetAuthenticationTokenAsync(user, configuration["AppSettings:AppName"], "RefreshToken", refreshToken);

        }
    }
}
