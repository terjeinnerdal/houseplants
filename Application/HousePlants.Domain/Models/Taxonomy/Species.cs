using System;

namespace HousePlants.Domain.Models.Taxonomy
{
    public class Species : TaxonomyBase, IEquatable<Species>
    {
        public Genus? Genus { get; set; }

        public Species(string name) : base(name)
        {
            Arcanea = Arcanea.Species;

        }

        public bool Equals(Species? other)
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

        public override int GetHashCode() => Id.GetHashCode();

    }
}