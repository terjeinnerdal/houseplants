using System;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models
{
    public sealed class Plant : BaseEntity
    {
        [StringLength(128)] public string? LatinName { get; set; } = default;
        [StringLength(128)] public string? CommonName { get; set; }

        [DataType(DataType.Date)]
        [Display(AutoGenerateField = true, AutoGenerateFilter = false, Prompt = "When did you aquire the plant")]
        public DateTime? AquiredDate { get; set; }

        // Climatic requirements
        public LightRequirement LightRequirement { get; set; }
        public WaterRequirement WaterRequirement { get; set; }
        public SoilRequirement SoilRequirement { get; set; }

        public Genus? Genus { get; set; }
    }
}
