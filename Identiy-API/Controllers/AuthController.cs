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
    private readonly ITokenServices tokenServices;
    private readonly IUniversityService universityService;
    private readonly IDeanService deanService;
    private readonly IStudentService studentService;
    private readonly UserManager<IdentityUser>  userManager;
    private readonly IAgregatorAuthFactory agregatorAuthFactory;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        ITokenServices tokenServices, 
        IUniversityService universityService, 
        IDeanService deanService,
        IStudentService studentService,
       UserManager<IdentityUser>  userManager ,
       IAgregatorAuthFactory agregatorAuthFactory
        )
    {
        this.authenticationService = authenticationService;
        this.tokenServices = tokenServices;
        this.universityService = universityService;
        this.deanService = deanService;
        this.studentService = studentService;
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

            Response.Cookies.Append("refreshToken", tokens.RefreshToken, cookieOptions);
            return Ok(new
            {
                Token = tokens.AccessToken,
                UserEmail = user.Email,
            });
        }
        catch (Exception)
        {

            throw;
        }
    

    }


    [HttpPost("/login-manager")]


    public async Task<IActionResult> LoginManager(LoginDTO loginDTO)
    {
        try
        {

            var user=await authenticationService.Login(loginDTO);
            
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


        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost("/login-dean")]
    public async Task<IActionResult> LoginDean(LoginDTO loginDTO)
    {
        try
        {

            var user = await authenticationService.Login(loginDTO);
            var ManagerIds = await deanService.GetDeanData(loginDTO);
             var accessToken = tokenServices.GetRefreshTokenDean(ManagerIds);
            var refreshToken = tokenServices.GetRefreshTokenDean(ManagerIds);
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
            });


        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost("/login-teacher")]
    public async Task<IActionResult> LoginTeacher(LoginDTO loginDTO)
    {
        try
        {

            var user = await authenticationService.Login(loginDTO);
            var ManagerIds = await universityService.GetTeacherData(loginDTO.Email);
            var accessToken = tokenServices.GetAccessTokenTeacher(ManagerIds);
            var refreshToken = tokenServices.GetRefreshTokenTeacher(ManagerIds);
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
            });


        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpPost("/login-student")]
    public async Task<IActionResult> LoginStudent(LoginDTO loginDTO)
    {
        try
        {

            var user = await authenticationService.Login(loginDTO);
            var ManagerIds = await studentService.GetStudentData(loginDTO.Email);
            var accessToken = tokenServices.GetAccessTokenStudent(ManagerIds);
            var refreshToken = tokenServices.GetRefreshTokenStudent(ManagerIds);
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
            });


        }
        catch (Exception)
        {

            throw;
        }
    }
}
