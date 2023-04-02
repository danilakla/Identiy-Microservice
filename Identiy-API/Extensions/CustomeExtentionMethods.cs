using Identiy_API.Services.Caching;
using Identiy_API.Services.CryptograthyService;
using Identiy_API.Services.MailService;
using Identiy_API.Services.TempSavingService;
using Identiy_API.Utils;

namespace Identiy_API.Extensions
{
    public static class CustomeExtentionMethods
    { 
        public static IServiceCollection AddTempRegistration(this IServiceCollection services)
        {
            services.AddSingleton<ITempRegistrationService, TempRegistratinon>();
            services.AddSingleton<ITempSaveDataService, RedisSaveBeforeConfirm>();
            services.AddSingleton<ICrypto, GeneratorTokenEmailConfirm>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<EmailMessages>();

            return services;
        }
    }
}
