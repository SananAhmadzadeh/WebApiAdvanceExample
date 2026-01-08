using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.DAL.UnitOfWork.Abstract;
using WebApiAdvanceExample.DAL.UnitOfWork.Concrete;
using WebApiAdvanceExample.Entities.Auth;

namespace WebApiAdvanceExample;

public static class ConfigurationServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddConfigurationService(IConfiguration configuration)
        {
            services.AddDbContext<WebApiAdvanceExampleDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddIdentity<AppUser<Guid>, IdentityRole>()
                .AddEntityFrameworkStores<WebApiAdvanceExampleDbContext>()
                .AddDefaultTokenProviders();

            var tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOption.Issuer,
                        ValidAudience = tokenOption.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
                        ClockSkew = TimeSpan.Zero,
                        
                    };
                });


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}