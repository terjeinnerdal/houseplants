using System;
using HousePlants.Areas.Identity.Data;
using HousePlants.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Extensions.Logging;
using HousePlantsDbContext = HousePlants.Areas.Identity.Data.HousePlantsDbContext;

[assembly: HostingStartup(typeof(HousePlants.Areas.Identity.IdentityHostingStartup))]
namespace HousePlants.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                var loggerFactory = new SerilogLoggerFactory();
                services.AddDbContext<HousePlantsDbContext>(options =>
                        options
                            .UseNpgsql(context.Configuration.GetConnectionString("PostgresConnection"), contextOptionsBuilder =>
                                contextOptionsBuilder.UseNodaTime())
                            .UseLoggerFactory(loggerFactory));

                services
                    .AddDefaultIdentity<HousePlantsUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 4;
                    })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<HousePlantsDbContext>();

                // Set fallback policy to require every user to be authenticated.
                services.AddAuthorization(options =>
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                });

                //services.AddScoped<IAuthorizationHandler,
                //    UserIsOwnerAuthorizationHandler>();
            });
        }
    }
}