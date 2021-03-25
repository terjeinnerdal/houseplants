#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models
{
    //[Flags]
    public enum WaterRequirement : short
    {
        None = 0,
        [Display(Name = "Wet/Dry cycle", Description = "Let the soil dry out before watering until runoff.")]
        WetDry = 1,
        [Display(Name = "Keep moist", Description = "Keep the soil constantly moist, but not too wet.")]
        KeepMoist = 2,
        [Display(Name = "Bone dry", Description = "Let the soil dry out completely before watering until runoff")]
        BoneDry = 4
    }
}
