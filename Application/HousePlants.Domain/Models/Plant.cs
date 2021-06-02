using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models
{
    public sealed class Plant : BaseEntity
    {
        [StringLength(128)] public string? LatinName { get; set; } = default;
        [StringLength(128)] public string? CommonName { get; set; }
        [DataType(DataType.Date)] public DateTime? AquiredDate { get; set; }
        
        public LegendStatus? LegendStatus { get; set; }
        public LightRequirement? LightRequirement { get; set; }
        public WaterRequirement? WaterRequirement { get; set; }
        public SoilRequirement? SoilRequirement { get; set; }
        public WateringTechnique WateringTechnique { get; set; }
        public int MinimumTemperature { get; set; }
        public int MaximumTemperature { get; set; }
        public bool Toxic { get; set; }
        public Classification? Classification { get; set; }
        public ICollection<PlantGroup>? Groups { get; set; }
    }
}
