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

        [HttpPost("/registr-user/temp-saving")]
        public async Task<IActionResult>  TempRegistrationUser([FromBody] CreateManagerDTO createManagerDTO)
        {
            try
            {

                await tempRegistrationService.RegistrationManager(createManagerDTO);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
