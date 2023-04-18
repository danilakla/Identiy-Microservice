using Identiy_API.DTO;
using Identiy_API.Model;
using Identiy_API.Services.CryptograthyService;
using Identiy_API.Services.RegistrationService;
using Identiy_API.Services;
using Identiy_API.Services.TempSavingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Identiy_API.Services.UniversityService;
using Identiy_API.Services.Caching;
using Newtonsoft.Json.Linq;
using Identiy_API.Services.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Identiy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ITempRegistrationService tempRegistrationService;
        private readonly ITokenServices tokenServices;
        private readonly IRegistrationService registrationService;
        private readonly ICrypto crypto;
        private readonly IStudentService studentService;
        private readonly IAuthenticationService authenticationService;
        private readonly UserManager<IdentityUser> userManager;

        public RegistrationController(ITempRegistrationService tempRegistrationService,
              ITokenServices tokenServices,
            IRegistrationService registrationService,
            ICrypto crypto,
            IStudentService studentService,
            IAuthenticationService authenticationService,
            UserManager<IdentityUser> userManager
            )
        {
            this.tempRegistrationService = tempRegistrationService;
            this.tokenServices = tokenServices;
            this.tokenServices = tokenServices;
            this.registrationService = registrationService;
            this.crypto = crypto;
            this.studentService = studentService;
            this.authenticationService = authenticationService;
            this.userManager = userManager;
        }

        [HttpPost("/registr-manager/temp-saving")]
        public async Task<IActionResult>  TempRegistrationManager([FromBody] CreateManagerDTO registrationUserDTO)
        {
            try
            {

                await tempRegistrationService.TempRegistration<CreateManagerDTO>(registrationUserDTO,registrationUserDTO.Role);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("/registr-user/temp-saving")]
        public async Task<IActionResult> TempRegistrationDean([FromBody] RegistrationUserDTO registratioUserDTO)
        {
            try
            {

                await tempRegistrationService.TempRegistration<RegistrationUserDTO>(registratioUserDTO, registratioUserDTO.Role);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("/registr-student")]
        public async Task<IActionResult> RegistrationStudent([FromBody] RegistrationUserDTO registratioUserDTO)
        {
            try
            {


                var hasAccount = await registrationService.IsRegistration(registratioUserDTO.Email);

                var data = crypto.DecryptSecretString<PayloadStudent>(registratioUserDTO.AuthenticationToken);
                if (data.Role != "Student") throw new Exception("Is not correct role");
                if (hasAccount)
                {
                    throw new Exception("User had account");
                }
                await registrationService.Registration(registratioUserDTO, "Student");

                var userIds = await studentService.InitStudent(new() { GroupId = data.GroupId, loginDTO = registratioUserDTO });
          

                var access = tokenServices.GetAccessTokenStudent(userIds,registratioUserDTO.Email);
                var refresh = tokenServices.GetRefreshTokenStudent(userIds, registratioUserDTO.Email);
                var user = await userManager.FindByEmailAsync(registratioUserDTO.Email);
                await authenticationService.SetRefreshToken(user, refresh);
                return Ok(new
                {
                    AccessToken = access,
                    RefreshToken = refresh,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public record class PayloadStudent
        {
            public int GroupId { get; set; }
            public string   Role { get; set; }

        }
    }
}
