using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using HousePlants.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HousePlants
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the generic host
            var host = CreateHostBuilder(args).Build();
            
            // Stuff to do before running the host
            CreateDbIfNotExists(host);

            // Run the host
            host.Run();
        }

        /// <summary>
        /// The code that calls CreateDefaultBuilder is in a method named CreateWebHostBuilder,
        /// which separates it from the code in Main that calls Run on the builder object. This
        /// separation is required if you use Entity Framework Core tools. The tools expect to
        /// find a CreateWebHostBuilder method that they can call at design time to configure the
        /// host without running the app. An alternative is to implement IDesignTimeDbContextFactory.
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureAppConfiguration(config =>
                        {
                            config
                                // Used for local settings like connection strings.
                                .AddJsonFile("appsettings.logs.json", optional: true);
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) => {
                            loggerConfiguration
                                .ReadFrom.Configuration(hostingContext.Configuration)
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                        });
                });
        
        
        private static void CreateDbIfNotExists(IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<HousePlantsContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB");
                throw;
            }
        }
    }
}
