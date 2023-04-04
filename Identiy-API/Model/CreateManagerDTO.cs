namespace Identiy_API.Model
{
    public class CreateManagerDTO:LoginDTO
    {
       
        public University University { get; set; }
        public string Role { get; set; }


    }
}
