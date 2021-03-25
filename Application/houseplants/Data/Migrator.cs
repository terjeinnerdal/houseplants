using EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HousePlants.Data
{
    /// <summary>
    /// Class for dotnet ef command.
    /// </summary>
    public class Migrator : IDesignTimeDbContextFactory<HousePlantsContext>
    {
        // Assembly where to place Migrations scaffolded code.
        const string MigrationsAssembly = nameof(HousePlants);


        public HousePlantsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HousePlantsContext>();
            var connectionStringBuilder =
                new SqlConnectionStringBuilder("Server=(localdb)\\MSSQLLocalDB;Database=HousePlants;Trusted_Connection=True;MultipleActiveResultSets=true");

            optionsBuilder.UseSqlServer(
                connectionStringBuilder.ConnectionString, builder =>
                {
                    builder.MigrationsAssembly(MigrationsAssembly);
                    builder.UseNodaTime();
                });

            // Sql Server (localdb)
            //_ = optionsBuilder.UseSqlServer(
            //    "Server=(localdb)\\MSSQLLocalDB;Database=HousePlants;Trusted_Connection=True;MultipleActiveResultSets=true",
            //    opt => opt.MigrationsAssembly(MigrationsAssembly)).UseNodaTime();

            // Postgres (cube)
            //optionsBuilder.UseNpgsql(
            //   "Host=cube;Database=pillars;Username=pillars;Password=uK8bzeWk",
            //   opt => opt.MigrationsAssembly(MigrationsAssembly));

            return new HousePlantsContext(optionsBuilder.Options);
        }
    }
}