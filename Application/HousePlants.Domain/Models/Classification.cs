namespace HousePlants.Domain.Models
{
    /// <summary>
    /// Important to register the substrate for each plant.
    /// Some plants grow, some don't, so repot and change the substrate until it thrives.
    /// This is valuable information over time.
    /// It will also allow you to know where that fungi came from.
    /// It will help you make your own judgement about which soil is the best.
    /// It will make you embarrased when you realize that all you need is the soil you buy.
    /// It will teach you about different types of soil.
    /// It will teach you what soil is, and why it is soil.
    /// It will teach you what the perched water table is, and why container size matters.
    /// 
    /// </summary>

    public class Classification : BaseEntity
    {
        public Family? Family { get; set; }
        public Genus? Genus { get; set; }
        public Species Species { get; set; }

    }
}