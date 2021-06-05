using System;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models.Requirements
{
    [Flags]
    public enum LightRequirement : short
    {
        None = 0,
        [Display(Name = "Shade")]
        Shade = 1,
        [Display(Name = "Indirect sunlight")]
        IndirectSunlight = 2,
        [Display(Name="Full sunlight")]
        FullSunlight = 4
    }
}
