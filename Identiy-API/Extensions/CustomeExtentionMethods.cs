using Identiy_API.Services.Caching;
using Identiy_API.Services.CryptograthyService;
using Identiy_API.Services.MailService;
using Identiy_API.Services.TempSavingService;
using Identiy_API.Services.UniversityService;
using Identiy_API.Utils;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Bcpg;
using UniversityApi.Protos;

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

        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUniversityService, UniversityService>();

            services.AddGrpcClient<University.UniversityClient>((services, options) =>
            {
                var universityApi = configuration["AppSettings:Grpc:UniversityApi"];
                options.Address = new Uri(universityApi);
            });


            services.AddScoped<IDeanService, DeanService>();

            services.AddGrpcClient<Dean.DeanClient>((services, options) =>
            {
                var universityApi = configuration["AppSettings:Grpc:UniversityApi"];
                options.Address = new Uri(universityApi);   
            });


            return services;
        }

    }
}
