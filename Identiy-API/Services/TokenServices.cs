using Identiy_API.Model;
using Identiy_API.Model.GrpcModel;
using Identiy_API.Model.Payload;
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
        readonly private int LIFE_TIME_TOKEN_HOUR = 100;//develop value



        private readonly IConfiguration _configuration;

        public TokenServices(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetAccessTokenDean(DeanPayload payload)
        {
            try
            {
                var DeanPayload = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Dean"),
                    new Claim("UniversityId", payload.UniversityId.ToString()),
                    new Claim("DeanId", payload.DeanId.ToString()),
                    new Claim("FacuiltieId", payload.FacultieId.ToString()),
                     new Claim("ProfileId", payload.DeanId.ToString()),


                };
                var token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(DeanPayload, LIFE_TIME_TOKEN_HOUR));
                return token;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string GetAccessTokenManager(ManagerPayload payload)
        {
            try
            {
                var ManagerClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, payload.Role),
                    new Claim("UniversityId",payload.payloadManagerDTO.UniversityId.ToString()),
                    new Claim("ManagerId", payload.payloadManagerDTO.ManagerId.ToString()),
                     new Claim("ProfileId", payload.payloadManagerDTO.ManagerId.ToString()),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var token = GenerateToken(ManagerClaim, LIFE_TIME_TOKEN_HOUR);
                var accToken = new JwtSecurityTokenHandler().WriteToken(token);
                return accToken;
            }
            catch (Exception e)
            {

                throw e;
            }
         
        }


        public string GetRefreshTokenDean(DeanPayload payload)
        {
            return GetAccessTokenDean(payload);
        }

        public string GetRefreshTokenManager(ManagerPayload payload)
        {
            try
            {
                return GetAccessTokenManager(payload);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetRefreshTokenTeacher(TeacherPayload payload)
        {
            return GetAccessTokenTeacher(payload);
        }


        public string GetAccessTokenTeacher(TeacherPayload payload)
        {
            try
            {
                var ManagerClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Teacher"),
                    new Claim("UniversityId",payload.UniversityId.ToString()),
                    new Claim("TeacherId", payload.TeacherId.ToString()),
                    new Claim("ProfileId", payload.TeacherId.ToString()),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var token = GenerateToken(ManagerClaim, LIFE_TIME_TOKEN_HOUR);
                var accToken = new JwtSecurityTokenHandler().WriteToken(token);
                return accToken;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private JwtSecurityToken GenerateToken(List<Claim> authClaims,int timeLives)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));

                var token = new JwtSecurityToken(

                    expires: DateTime.Now.AddHours(timeLives),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public string GetAccessTokenStudent(StudentPayload payload)
        {
            try
            {
                var studentClaims= new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Student"),
                    new Claim("UniversityId",payload.UniversityId.ToString()),
                    new Claim("GroupId", payload.GroupId.ToString()),
                    new Claim("FacuiltieId", payload.FacultieId.ToString()),
                    new Claim("ProfessionId", payload.ProfessionId.ToString()),
                    new Claim("StudentId", payload.StudentId.ToString()),
                    new Claim("ProfileId", payload.StudentId.ToString()),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var token = GenerateToken(studentClaims, LIFE_TIME_TOKEN_HOUR);
                var accToken = new JwtSecurityTokenHandler().WriteToken(token);
                return accToken;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string GetRefreshTokenStudent(StudentPayload payload)
        {
            return GetAccessTokenStudent(payload);
        }
    }
}
