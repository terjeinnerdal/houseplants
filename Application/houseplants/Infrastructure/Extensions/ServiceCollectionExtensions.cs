//using HousePlants.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Npgsql;
//using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

//namespace HousePlants.Infrastructure.Extensions
//{
    //    /// <summary>
    //    /// Class for dotnet ef command.
    //    /// </summary>
    //    public class Migrator : IDesignTimeDbContextFactory<HousePlantsContext>
    //    {
    //        // Assembly where to place Migrations scaffolded code.
    //        const string MigrationsAssembly = nameof(HousePlants);


    //        public HousePlantsContext CreateDbContext(string[] args)
    //        {
    //            var optionsBuilder = new DbContextOptionsBuilder<HousePlantsContext>();
    //            var connectionStringBuilder =
    //                new NpgsqlConnectionStringBuilder();

    //            connectionStringBuilder.Database = "HousePlants";
    //            connectionStringBuilder.Host = "postgres";

    //            optionsBuilder.UseSqlServer(
    //                connectionStringBuilder.ConnectionString, builder =>
    //                {
    //                    builder.MigrationsAssembly(MigrationsAssembly);
    //                    builder.UseNodaTime();
    //                });

    //            // Sql Server (localdb)
    //            //_ = optionsBuilder.UseSqlServer(
    //            //    "Server=(localdb)\\MSSQLLocalDB;Database=HousePlants;Trusted_Connection=True;MultipleActiveResultSets=true",
    //            //    opt => opt.MigrationsAssembly(MigrationsAssembly)).UseNodaTime();

    //            // Postgres (cube)
    //            //optionsBuilder.UseNpgsql(
    //            //   "Host=cube;Database=pillars;Username=pillars;Password=uK8bzeWk",
    //            //   opt => opt.MigrationsAssembly(MigrationsAssembly));

    //            return new HousePlantsContext(optionsBuilder.Options);
    //        }
    //    }

//    public static class ServiceCollectionExtensions
//    {
//        public static IServiceCollection AddDbContextWithPostgresAndNodatime<TContext>(
//            this IServiceCollection services,
//            string connectionStringName = "PostgresConnection",
//            NpgsqlDbContextOptionsBuilder options = null)
//            where TContext : DbContext
//        {
//            var config = services
//               .BuildServiceProvider()
//               .GetRequiredService<IConfiguration>();

//            var connectionString = config.GetConnectionString(connectionStringName);

//            const string hostEnvName = "POSTGRES_HOST";
//            var connectionstringContainsHostEnvironmentVariable = connectionString.Contains(hostEnvName);
//            var sqlHostFromConfiguration = config.GetValue<string>(hostEnvName);
//            if (!string.IsNullOrWhiteSpace(sqlHostFromConfiguration) && connectionstringContainsHostEnvironmentVariable)
//            {
//                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
//                {
//                    Host = sqlHostFromConfiguration
//                };
//                connectionString = connectionStringBuilder.ConnectionString;
//            }

//            const string dbPasswordEnvName = "DATABASE_USER_PWD";
//            var connectionstringContainsPasswordEnvironmentVariable = connectionString.Contains(dbPasswordEnvName);
//            var dbPasswordFromConfiguration = config.GetValue<string>(dbPasswordEnvName);
//            if (!string.IsNullOrWhiteSpace(dbPasswordFromConfiguration) &&
//                connectionstringContainsPasswordEnvironmentVariable)
//            {
//                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
//                {
//                    Password = dbPasswordFromConfiguration
//                };
//                connectionString = connectionStringBuilder.ConnectionString;
//            }

//            options!.UseNodaTime();
//            services.AddDbContext<HousePlantsContext>(opt =>
//                opt.UseNpgsql(connectionString));

//            return services;
//        }

//        private static void ConfigureDbContext(
//            DbContextOptionsBuilder options,
//            IServiceCollection services,
//            string connectionStringName,
//            bool useNodaTime,
//            ILoggerFactory loggerFactory)
//        {
//            var config = services
//                .BuildServiceProvider()
//                .GetRequiredService<IConfiguration>();

//            var connectionString = config.GetConnectionString(connectionStringName);

//            const string hostEnvName = "POSTGRES_HOST";
//            var connectionstringContainsHostEnvironmentVariable = connectionString.Contains(hostEnvName);
//            var sqlHostFromConfiguration = config.GetValue<string>(hostEnvName);
//            if (!string.IsNullOrWhiteSpace(sqlHostFromConfiguration) && connectionstringContainsHostEnvironmentVariable)
//            {
//                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
//                {
//                    Host = sqlHostFromConfiguration
//                };
//                connectionString = connectionStringBuilder.ConnectionString;
//            }

//            const string dbPasswordEnvName = "DATABASE_USER_PWD";
//            var connectionstringContainsPasswordEnvironmentVariable = connectionString.Contains(dbPasswordEnvName);
//            var dbPasswordFromConfiguration = config.GetValue<string>(dbPasswordEnvName);
//            if (!string.IsNullOrWhiteSpace(dbPasswordFromConfiguration) &&
//                connectionstringContainsPasswordEnvironmentVariable)
//            {
//                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
//                {
//                    Password = dbPasswordFromConfiguration
//                };
//                connectionString = connectionStringBuilder.ConnectionString;
//            }

//            options.UseNpgsql(
//                connectionString,
//                postgresOptionsBuilder =>
//                {
//                    //postgresOptionsBuilder.EnableRetryOnFailure(3);
//                    if (useNodaTime)
//                    {
//                        postgresOptionsBuilder.UseNodaTime();
//                    }
//                });


//            if (loggerFactory != null)
//            {
//                options.UseLoggerFactory(loggerFactory);
//            }
//        }
//    }
//}

//}