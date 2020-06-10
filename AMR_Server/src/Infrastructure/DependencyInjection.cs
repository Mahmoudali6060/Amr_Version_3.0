using AMR_Server.Application.Common.Interfaces;
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
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<AmrDbContext>(options =>
            //        options.UseInMemoryDatabase("Clean_ArchitectureDb"));
            //}
            //else
            //{
            //    services.AddDbContext<AmrDbContext>(options =>
            //        options.UseSqlServer(
            //            configuration.GetConnectionString("DefaultConnection"),
            //            b => b.MigrationsAssembly(typeof(AmrDbContext).Assembly.FullName)));
            //}
            services.AddDbContext<AmrDbContext>(options =>
                                      
                  options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAmrDbContext>(provider => provider.GetService<AmrDbContext>());

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<AmrDbContext>();
            
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AmrDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
