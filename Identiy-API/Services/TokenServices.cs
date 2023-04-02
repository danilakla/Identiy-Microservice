using Identiy_API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace Identiy_API.Services
{
    public class TokenServices : ITokenServices
    {




        private readonly IConfiguration _configuration;

        public TokenServices(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetAccessToken(IdentityUser payload,string Role)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, payload.UserName),
                    new Claim(ClaimTypes.Role, Role),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return "";

        }

        public string GetAccessTokenManager(CreateManagerDTO payload, string Role)
        {
            var ManagerClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, payload.Name+" "+payload.LastName),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(ClaimTypes.Role, Role),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            ManagerClaim.Where(e => e.Type == "ds");

            throw new NotImplementedException();
        }

  

        public string GetRefreshTokenManager(CreateManagerDTO payload)
        {
            throw new NotImplementedException();
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims,int timeLives)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddHours(timeLives),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
