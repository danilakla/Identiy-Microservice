using Identiy_API.Model.Payload;
using Identiy_API.Services;
using Identiy_API.Services.Caching;
using Identiy_API.Services.RegistrationService;
using Identiy_API.Services.UniversityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identiy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmEmailController : ControllerBase
    {
        private readonly IUniversityService universityService;
        private readonly ITempSaveDataService tempSaveDataService;
        private readonly ITokenServices tokenServices;
        private readonly IRegistrationService registrationService;

        public ConfirmEmailController(IUniversityService universityService , 
            ITempSaveDataService tempSaveDataService, 
            ITokenServices tokenServices,
            IRegistrationService registrationService
            
            )
        {
            this.universityService = universityService;
            this.tempSaveDataService = tempSaveDataService;
            this.tokenServices = tokenServices;
            this.registrationService = registrationService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> Confirm(string token)
        {

            try
            {

                var manager = await tempSaveDataService.GetManager(token);

                var hasAccount = await registrationService.IsRegistration(manager.Email);
                if (hasAccount)
                {
                    throw new Exception("User had account");
                }
                await registrationService.RegistrationManager(manager);

                var userIds =await universityService.InitUniversity(manager);
                var payload = new ManagerPayload { payloadManagerDTO = userIds, Role = "Manager" };


                return Redirect("https://www.youtube.com/");

            }
            catch (Exception)
            {

                throw;
            }
        
        }
    }
}
