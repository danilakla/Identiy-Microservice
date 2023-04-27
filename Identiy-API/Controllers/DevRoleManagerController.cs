using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Identiy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevRoleManagerController : ControllerBase
    {
        private RoleManager<IdentityRole> roleManager;
        public DevRoleManagerController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        [HttpPost("/add-role")]
        public async Task AddRole(string role)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
