using Identiy_API.Model;

namespace Identiy_API.DTO
{
    public class RegistrationUserDTO:LoginDTO
    {
        public string AuthenticationToken { get; set; }
    }
}
