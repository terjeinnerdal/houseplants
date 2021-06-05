using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HousePlants.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousePlants.Pages.Plants
{
    public class PlantDto
    {
        [HiddenInput, Key]
        public Guid Id { get; private set; }
        public string Title { get; set; }
        [StringLength(128)] public string LatinName { get; set; }
        [StringLength(128)] public string CommonName { get; set; }
        [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
        //public LightRequirement? LightRequirement { get; set; }
        //public WaterRequirement? WaterRequirement { get; set; }
        //public SoilRequirement? SoilRequirement { get; set; }
        //public WateringTechnique? WateringTechnique { get; set; }
        //public int? MinimumTemperature { get; set; }
        //public Family Family { get; set; }
        //public Genus Genus { get; set; }
        //public Species Species { get; set; }
        //public Legends? Legends { get; set; }
        //List<string> Tags { get; set; }

        // public string TagsString
        // {
        //     get => string.Join(',', Tags);
        //     set
        //     {
        //         Tags = value != null
        //             ? TagsString.Split(',').Select(tag => tag.Trim()).ToList()
        //             : throw new ArgumentNullException(nameof(value));
        //     }
        // }
    }
}