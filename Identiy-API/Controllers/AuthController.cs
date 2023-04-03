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
    private readonly ITokenServices tokenServices;
    private readonly IUniversityService universityService;

    public AuthenticationController(IAuthenticationService authenticationService, ITokenServices tokenServices, IUniversityService universityService)
    {
        this.authenticationService = authenticationService;
        this.tokenServices = tokenServices;
        this.universityService = universityService;
    }

    [HttpPost("/login-manager")]
    public async Task<IActionResult> LoginManager(LoginDTO loginDTO)
    {
        try
        {

            var user=await authenticationService.LoginManager(loginDTO);
            var ManagerIds=await universityService.GetManagerData(loginDTO);
            var managerPayload = new ManagerPayload { payloadManagerDTO = ManagerIds, Role = "Manager" };
            var accessToken = tokenServices.GetAccessTokenManager(managerPayload);
            var refreshToken = tokenServices.GetRefreshTokenManager(managerPayload);
            await authenticationService.SetRefreshToken(user, refreshToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,

            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            return Ok(new
            {
                Token = accessToken,
                UserEmail = user.Email,
            }) ;


            return Ok();
        }
        catch (Exception)
        {

            throw;
        }
    }

    }
