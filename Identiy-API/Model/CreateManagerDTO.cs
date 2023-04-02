namespace Identiy_API.Model
{
    public class CreateManagerDTO
    {
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
    
        public string Password { get; set; }

        public University University { get; set; }
        public string Role { get; set; }


    }
}
