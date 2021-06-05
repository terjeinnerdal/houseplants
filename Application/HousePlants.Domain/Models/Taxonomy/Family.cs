namespace HousePlants.Domain.Models.Taxonomy
{
    public class Family : TaxonomyBase
    {
        public Family(string name) : base(name)
        {
            Arcanea = Arcanea.Family;
        }
    }
}