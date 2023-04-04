using Identiy_API.DTO;
using Identiy_API.Model;
using Identiy_API.Services.TempSavingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identiy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ITempRegistrationService tempRegistrationService;

        public RegistrationController(ITempRegistrationService tempRegistrationService)
        {
            this.tempRegistrationService = tempRegistrationService;
        }

        [HttpPost("/registr-manager/temp-saving")]
        public async Task<IActionResult>  TempRegistrationManager([FromBody] CreateManagerDTO registrationUserDTO)
        {
            try
            {

                await tempRegistrationService.TempRegistration<CreateManagerDTO>(registrationUserDTO);

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

                await tempRegistrationService.TempRegistration<RegistrationUserDTO>(registratioUserDTO);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
