using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Infrastructure.Configurations;
using AMR_Server.Infrastructure.Identity;
using AMR_Server.Infrastructure.Persistence;
using AMR_Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMR_Server.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Settings>(configuration.GetSection("Settings"));//To set Settings section from AppSettings.json to Settings class

            services.AddDbContext<AmrDbContext>(options =>
                  options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IdentityDbContext>(options =>
           options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAmrDbContext>(provider => provider.GetService<AmrDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, IdentityDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            //services.AddTransient<ISettings, Settings>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
