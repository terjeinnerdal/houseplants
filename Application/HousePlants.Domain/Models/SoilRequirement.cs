using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models
{
    public enum SoilRequirement : short
    {
        None = 0,
        [Display(Name = "Well draining mix")]
        WellDraining = 1,
        [Display(Name = "Sandy mix")]
        Sandy = 2,
        [Display(Name = "Coarse mix")]
        Coarse = 4,
        [Display(Name="Standard potting mix")]
        StandardPottingMix = 8
    }
}