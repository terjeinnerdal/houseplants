namespace HousePlants.Domain.Models.Taxonomy
{
    #region Probably never gonna use
    //public class Class : TaxonomyBase
    //{
    //    public override Arcanea Arcanea => Arcanea.Class;
    //}

    //public class Order : TaxonomyBase
    //{
    //    public override Arcanea Arcanea => Arcanea.Order;
    //}
    //public class Variety : TaxonomyBase
    //{
    //    public override Arcanea Arcanea => Arcanea.Variety;
    //}
    #endregion

    public class Genus : TaxonomyBase
    {
        public Family? Family { get; set; }

        public Genus(string name) : base(name)
        {
            Arcanea = Arcanea.Genus;
        }
    }
}