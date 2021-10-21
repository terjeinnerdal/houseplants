using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HousePlants.Models.Interfaces;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Taxonomy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace HousePlants.Areas.Identity.Data
{
    public class HousePlantsDbContext : IdentityDbContext<HousePlantsUser>
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Genus> Genus { get; set; }
        public DbSet<Family> Families { get; set; }

        public HousePlantsDbContext(DbContextOptions<HousePlantsDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is INodaTimeEntity &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((INodaTimeEntity)entityEntry.Entity).Modified = SystemClock.Instance.GetCurrentInstant();

                if (entityEntry.State == EntityState.Added)
                {
                    ((INodaTimeEntity)entityEntry.Entity).Created = SystemClock.Instance.GetCurrentInstant();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
