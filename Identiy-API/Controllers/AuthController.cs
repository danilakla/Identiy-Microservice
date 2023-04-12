using Identiy_API.Infrastructure.AuthFactory.AuthAgregatorFactory;
using Identiy_API.Model;
using Identiy_API.Model.Payload;
using Identiy_API.Services;
using Identiy_API.Services.Authentication;
using Identiy_API.Services.UniversityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identiy_API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
    private readonly IAuthenticationService authenticationService;
    
    private readonly UserManager<IdentityUser>  userManager;
    private readonly IAgregatorAuthFactory agregatorAuthFactory;

    public AuthenticationController(
        IAuthenticationService authenticationService,
       UserManager<IdentityUser>  userManager ,
       IAgregatorAuthFactory agregatorAuthFactory
        )
    {
        this.authenticationService = authenticationService;
      
        this.userManager = userManager;
        this.agregatorAuthFactory = agregatorAuthFactory;
    }



    [HttpPost("Login-user")]
    public async Task<IActionResult> LoginUser (LoginDTO loginDTO){
        try
        {
            var user = await authenticationService.Login(loginDTO);
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            var authService = agregatorAuthFactory.GetAuthService(role);
            var tokens = await authService.AuthenticateUser(loginDTO);
            await authenticationService.SetRefreshToken(user, tokens.RefreshToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,

            };

            return Ok(new
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            });
        }
        catch (Exception)
        {

            throw;
        }
    

    }


}
