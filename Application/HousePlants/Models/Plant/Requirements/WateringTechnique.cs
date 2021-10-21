using System.ComponentModel.DataAnnotations;

namespace HousePlants.Models.Plant.Requirements
{
    public enum WaterRequirement : short
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 4
    }
    
    public enum WateringTechnique
    {
        None = 0,
        [Display(Name = "Wet/Dry cycle", Description = "Let the top 3-5 cm off soil dry out, before watering thorooughly.")]
        WetDry = 1,
        [Display(Name = "Keep moist", Description = "Keep the soil moist, but not too wet.")]
        KeepMoist = 2,
        [Display(Name = "Bone dry", Description = "Let the soil dry out completely, then water until runoff.")]
        WetBoneDry = 4
    }

}
