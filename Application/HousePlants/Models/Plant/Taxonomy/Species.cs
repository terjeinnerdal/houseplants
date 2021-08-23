using System;

#nullable enable
namespace HousePlants.Models.Plant.Taxonomy
{
    public class Species : TaxonomyBase
    {
        public Genus Genus { get; set; } = default!;
        public PlantPassport PlantPassport { get; set; } = PlantPassport.StandardPassport;
        
        public Species(string name) : base(name)
        {
        }

        public Species(string name, Genus genus) : base(name)
        {
            Genus = genus;
        }
        public Species(string name, Genus genus, PlantPassport plantPassport) : base(name)
        {
            Genus = genus;
            PlantPassport = plantPassport;
        }
    }

    public class Variety : TaxonomyBase
    {
        public Species Species { get; set; } = default!;
        public PlantPassport PlantPassport { get; set; } = PlantPassport.StandardPassport;
        public Variety(string name) : base(name)
        {
        }
        public Variety(string name, Species species) : base(name)
        {
            Species = species;
        }
        public Variety(string name, Species species, PlantPassport plantPassport) : base(name)
        {
            Species = species;
            PlantPassport = plantPassport;
        }
    }
}