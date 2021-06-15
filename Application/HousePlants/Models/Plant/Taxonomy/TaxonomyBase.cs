#nullable enable
using System;

namespace HousePlants.Models.Plant.Taxonomy
{


    // Family -> Genus -> Species -> (Variety) -> Plants
    // Morocaea -> 
    public abstract class TaxonomyBase : IEquatable<TaxonomyBase>
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        protected TaxonomyBase(string name)
        {
            Name = name;
        }
        
        public bool Equals(TaxonomyBase? other)
        {
            return Name == other?.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Species) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}