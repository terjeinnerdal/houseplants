using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HousePlants.Models.Requirements;
using HousePlants.Models.Taxonomy;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace HousePlants.Models
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

    public enum NutrientRequirement
    {
        Low,
        Medium,
        High
    }

    // User owned entity
    public class Plant : BaseEntity
    {
        // This is Species, most people proably just want to write ficus lyrata.
        [StringLength(128)] public string? LatinName => Species?.Name;
        [StringLength(128)] public string? CommonName { get; set; }
        public DateTime? AquiredDate { get; set; }
        public int MinimumTemperature { get; set; }
        public int MaximumTemperature { get; set; }

        public Species? Species { get; set; }
        public IList<Tag>? Tags { get; set; } = new List<Tag>();

        public override string Title
        {
            get
            {
                string? title;
                if (!string.IsNullOrEmpty(LatinName))
                {
                    title = LatinName;
                }
                else if (!string.IsNullOrEmpty(CommonName))
                {
                    title = CommonName;
                }
                else
                {
                    title = "Specify LatinName or CommonName";
                }

                return title ?? string.Empty;
            }
            set => _ = value;
        }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Plant> Plants { get; set; } = new List<Plant>();

        public Tag(string name)
        {
            Name = name;
        }
    }
}
