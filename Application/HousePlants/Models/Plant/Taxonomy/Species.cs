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
}