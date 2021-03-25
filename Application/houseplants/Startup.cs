using EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using HousePlants.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HousePlants
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Enable Application Insights for telemetries. Update the instrumentation key in 'appsettings.json' to transfer the events.
            //services.AddApplicationInsightsTelemetry();
            //services.AddApplicationInsightsKubernetesEnricher();

            services.AddRazorPages();

            //services.AddDbContext<HousePlantsContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("HousePlantsContext")));

            services.AddDbContext<HousePlantsContext>(options =>
            {
                var connectionStringBuilder =
                    new SqlConnectionStringBuilder(Configuration.GetConnectionString("HousePlantsContext"));

                options.UseSqlServer(
                    connectionStringBuilder.ConnectionString, builder => builder.UseNodaTime());
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
