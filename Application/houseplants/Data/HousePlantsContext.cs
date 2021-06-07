using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HousePlants.Models;
using HousePlants.Models.Taxonomy;
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
                e.Entity is BaseEntity && 
                (e.State == EntityState.Added ||
                e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).Modified = SystemClock.Instance.GetCurrentInstant();

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).Created = SystemClock.Instance.GetCurrentInstant();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
