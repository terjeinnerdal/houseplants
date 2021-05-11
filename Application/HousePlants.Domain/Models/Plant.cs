using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public bool Toxic { get; set; }
        public Classification? Classification { get; set; }
        public ICollection<PlantGroup>? Groups { get; set; }
    }

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
    }
}
