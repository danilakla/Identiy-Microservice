using Identiy_API.Model;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegistrationService(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task RegistrationManager(CreateManagerDTO createManagerDTO)
        {
            try
            {


                var isFind =await userManager.FindByEmailAsync(createManagerDTO.Email);
                if (isFind is not null)
                {
                    throw new Exception("User is exist");
                }

                IdentityUser user = new() { UserName=createManagerDTO.Name+createManagerDTO.LastName, Email=createManagerDTO.Email};

                var userSaved=await userManager.CreateAsync(user,createManagerDTO.Password);


            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
