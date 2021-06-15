using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing.Printing;
using System.Linq;
using HousePlants.Data;
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
        private static readonly string SQL_HOST_FORMAT = "{{ENV_SQL_HOST}}";
        private static readonly string SQL_USERNAME_FORMAT = "{{ENV_SQL_USERNAME}}";
        private static readonly string SQL_PASSWORD_FORMAT = "{{ENV_SQL_PASSWORD}}";

        private static readonly List<string>  RequiredEnvVars = new []
        {
            "ENV_SQL_HOST",
            "ENV_SQL_USERNAME",
            "ENV_SQL_PASSWORD",
            "ConnectionStrings:PostgresConnection",
            "ASPNETCORE_ENVIRONMENT",
            "ASPNETCORE_ULRS",
            "ASPNETCORE_LOGGING_CONSOLE_DISABLECOLORS",
            "DOTNET_VERSION",
            "DOTNET_RUNNING_IN_CONTAINER",
            "ENVIRONMENT",
            "ASPNETCORE_VERSION"
        }.ToList();
        
        //private static void CheckConfig(IConfiguration config)
        //{
        //    foreach (string requiredEnvVar in RequiredEnvVars)
        //    {
        //        CheckSetting(config, requiredEnvVar);
        //    }
        //}

        //private static void CheckSetting(IConfiguration config, string key)
        //{
        //    if (string.IsNullOrEmpty(config[key]))
        //    {
        //        //                throw new ConfigurationException($"{key} must be specified");
        //    }
        //}

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            var config = services.GetRequiredService<IConfiguration>();

            PrintEnv();
            void PrintEnv()
            {
                Console.WriteLine($"Printing RequiredEnvVars");
                var sortedEnvVars = config.AsEnumerable()
                    .Where(envVarKvp => RequiredEnvVars.Any(required => Equals($"\"{required}\"", $"\"{envVarKvp.Key}\"")))
                    .ToImmutableDictionary();
                foreach (var keyValuePair in sortedEnvVars)
                {
                    if (RequiredEnvVars.Any(s=> keyValuePair.Key.Contains(s)))
                    {
                        Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
                    }
                }
            }

            //CheckConfig();
            //void CheckConfig()
            //{
            //    foreach (string requiredEnvVar in RequiredEnvVars)
            //    {
            //        CheckSetting(config, requiredEnvVar);
            //    }
            //}

            CreateDbIfNotExists(host);
            void CreateDbIfNotExists(IHost host)
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
                        .ConfigureServices((context, services) =>
                        {
                            ReplaceConnectionStringValues();
                            void ReplaceConnectionStringValues()
                            {
                                string sqlHost = context.Configuration["ENV_SQL_HOST"];
                                string username = context.Configuration["ENV_SQL_USERNAME"];
                                string password = context.Configuration["ENV_SQL_PASSWORD"];

                                string defaultConnectionString = context.Configuration.GetConnectionString("PostgresConnection");

                                var defaultConnectionStringBuilder =
                                    new Npgsql.NpgsqlConnectionStringBuilder(defaultConnectionString);

                                if (defaultConnectionString.Contains(SQL_HOST_FORMAT))
                                {
                                    defaultConnectionStringBuilder.Host = sqlHost;
                                }

                                if (defaultConnectionString.Contains(SQL_USERNAME_FORMAT))
                                {
                                    defaultConnectionStringBuilder.Username = username;
                                }

                                if (defaultConnectionString.Contains(SQL_PASSWORD_FORMAT))
                                {
                                    defaultConnectionStringBuilder.Password = password;
                                }

                                context.Configuration["ConnectionStrings:PostgresConnection"] =
                                    defaultConnectionStringBuilder.ConnectionString;
                            }
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
                });
    }
}
