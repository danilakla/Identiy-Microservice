namespace Identiy_API.Utils
{
    public class EmailMessages
    {
        private readonly IConfiguration configuration;

        public EmailMessages(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string BodyHtml(string msg)
        {
            string url = $"{configuration["AppSettings:ApiUrlEmailConfirm"]}{msg}";
            var bodyHTML = $"pls click to link <a href='{url}'>confirm email</a>";
            return bodyHTML;
        }
    }
}
