namespace Identiy_API.Model
{
    public class EmailDto
    {
        public  string Body { get; set; }
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
