namespace HousePlants.Domain.Models
{
    /// <summary>
    /// Class containing the data the plant was shipped with. This is a value type. It never changes after creation.
    /// </summary>
    public class Passport
    {
        public short MinimumTemperature { get; set; }
        public short MaximumTemperature { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public WateringTechnique WateringTechnique{ get; set; }

        public enum WaterScale
        {
            Low, 
            Medium, 
            High
        }
    }
}