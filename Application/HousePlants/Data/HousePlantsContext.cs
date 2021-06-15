using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Taxonomy;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace HousePlants.Data
{
    public class HousePlantsContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Genus> Genus { get; set; }
        public DbSet<Family> Families { get; set; }




        public HousePlantsContext (DbContextOptions<HousePlantsContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Updates the Created and Modified timestamps for BaseEntity types.
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries().Where(e =>
                e.Entity is GuidAndNodaTimeEntityBase && 
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((GuidAndNodaTimeEntityBase)entityEntry.Entity).Modified = SystemClock.Instance.GetCurrentInstant();

                if (entityEntry.State == EntityState.Added)
                {
                    ((GuidAndNodaTimeEntityBase)entityEntry.Entity).Created = SystemClock.Instance.GetCurrentInstant();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
