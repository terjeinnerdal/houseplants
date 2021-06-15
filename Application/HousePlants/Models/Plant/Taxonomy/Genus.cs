#nullable enable
namespace HousePlants.Models.Plant.Taxonomy
{
    public class Genus : TaxonomyBase
    {
        public Family? Family { get; set; }
        public PlantPassport? PlantPassport { get; set; }

        public Genus(string name) : base(name) { }

        public Genus(string name, Family family) : base(name)
        {
            Family = family;
        }
    }
}