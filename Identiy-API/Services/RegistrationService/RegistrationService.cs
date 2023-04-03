using Identiy_API.Model;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public RegistrationService(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<bool> IsRegistration(string email)
        {
            try
            {

                var result = await userManager.FindByEmailAsync(email);

                if (result is not null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RegistrationManager(CreateManagerDTO createManagerDTO)
        {
            try
            {


    

                IdentityUser user = new() { UserName=createManagerDTO.Name,Email=createManagerDTO.Email};

                
                var userSaved=await userManager.CreateAsync(user,createManagerDTO.Password);
                if (!userSaved.Succeeded)
                {
                    throw new Exception( "401");
                }


            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
    }
}
