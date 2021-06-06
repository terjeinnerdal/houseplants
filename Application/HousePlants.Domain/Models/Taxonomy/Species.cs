﻿using System;

namespace HousePlants.Domain.Models.Taxonomy
{
    public class Species : TaxonomyBase, IEquatable<Species>
    {
        public Genus Genus { get; set; }
        public PlantPassport PlantPassport { get; set; }
        
        public Species(string name) : base(name, Arcanea.Species)
        {
            PlantPassport = PlantPassport.StandardPassport;
        }
        public Species(string name, Genus genus) : base(name, Arcanea.Species)
        {
            Genus = genus;
            PlantPassport = PlantPassport.StandardPassport;
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