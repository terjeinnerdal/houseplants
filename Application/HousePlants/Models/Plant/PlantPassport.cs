using HousePlants.Models.Plant.Requirements;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace HousePlants.Models.Plant
{
    [Owned]
    public class PlantPassport
    {
        public bool? Perennial { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public WaterRequirement WaterRequirement { get; set; }
        public NutrientRequirement NutrientRequirement { get; set; }
        public SoilRequirement SoilRequirement { get; set; }
        public WateringTechnique WateringTechnique { get; set; }
        public int? MinimumTemperature { get; set; }
        public bool? Edible { get; set; }
        public FloweringPeriod? FloweringPeriod { get; set; }
        public int? Height { get; set; }

        public static PlantPassport StandardPassport => new PlantPassport
        {
            LightRequirement = LightRequirement.IndirectSunlight,
            WaterRequirement = WaterRequirement.Medium,
            NutrientRequirement = NutrientRequirement.Medium,
            SoilRequirement = SoilRequirement.StandardMix,
            WateringTechnique = WateringTechnique.WetDry,
            MinimumTemperature = 15
        };

        public static PlantPassport SucculentPassport => new PlantPassport
        {
            LightRequirement = LightRequirement.FullSunlight,
            WaterRequirement = WaterRequirement.Low,
            NutrientRequirement = NutrientRequirement.Low,
            SoilRequirement = SoilRequirement.Sandy,
            WateringTechnique = WateringTechnique.WetBoneDry,
            MinimumTemperature = 10
        };
    }
}