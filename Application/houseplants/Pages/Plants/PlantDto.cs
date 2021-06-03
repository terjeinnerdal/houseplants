using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HousePlants.Domain.Models;

namespace HousePlants.Pages.Plants
{
    public class PlantDto
    {
        [StringLength(128)] public string LatinName { get; set; }
        [StringLength(128)] public string CommonName { get; set; }
        [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
        public LightRequirement? LightRequirement { get; set; }
        public WaterRequirement? WaterRequirement { get; set; }
        public SoilRequirement? SoilRequirement { get; set; }
        public WateringTechnique? WateringTechnique { get; set; }
        public int? MinimumTemperature { get; set; }
        public int? MaximumTemperature { get; set; }
        public Family Family { get; set; }
        public Genus Genus { get; set; }
        public Species Species { get; set; }
        public LegendStatus? LegendStatus { get; set; }
        internal IEnumerable<string> Tags { get; set; }

        public string TagsString
        {
            get => string.Join(',', Tags);
            set
            {
                Tags = value != null
                    ? TagsString.Split(',').Select(tag => tag.Trim())
                    : throw new ArgumentNullException(nameof(value));
            }
        }
    }
}