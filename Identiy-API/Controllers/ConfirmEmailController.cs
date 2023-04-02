using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identiy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmEmailController : ControllerBase
    {


        [HttpGet("{token}")]
        public async Task<IActionResult> Confirm(string token)
        {
            return Redirect("https://www.youtube.com/");
        }
    }
}
