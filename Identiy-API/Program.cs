using Identiy_API.Data;
using Identiy_API.Extensions;
using Identiy_API.Services;
using Identiy_API.Services.Authentication;
using Identiy_API.Services.RegistrationService;
using Identiy_API.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

//custom
builder.Services.AddTempRegistration();
builder.Services.AddGrpcServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITokenServices, TokenServices>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("connMSSQL")));
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = configuration["AppSettings:Redis"];
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDataProtection().SetApplicationName("Chat")
            .AddKeyManagementOptions(options =>
            {
                options.AutoGenerateKeys = false;
            });
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuerSigningKey = false,
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime= false,
        ValidateLifetime=true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]))
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


