using System.ComponentModel.DataAnnotations;

namespace HousePlants.Models.Plant.Requirements
{
    public enum LightRequirement : short
    {
        None = 0,
        [Display(Name = "Shade")]
        Shade = 1,
        [Display(Name = "Indirect sunlight")]
        IndirectSunlight = 4,
        [Display(Name="Full sunlight")]
        FullSunlight = 8
    }
}
