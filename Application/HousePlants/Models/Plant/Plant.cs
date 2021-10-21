using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HousePlants.Models.Interfaces;
using HousePlants.Models.Plant.Taxonomy;
using NodaTime;

#nullable enable
namespace HousePlants.Models.Plant
{
    public class Plant : HousePlantsEntityBase
    {
        [StringLength(128)] public string? LatinName { get; set; }
        [StringLength(128)] public string? CommonName { get; set; }
        public DateTime? AquiredDate { get; set; }
        public int MinimumTemperature { get; set; } = 15;
        public Species? Species { get; set; }
        // public IList<Tag>? Tags { get; set; } = new List<Tag>();
        // public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();

        public Plant(string owner) : base(owner)
        {
        }

        public int CompareTo(Plant? other)
        {
            return string.Compare(Title, other?.Title, StringComparison.Ordinal);
        }
    }
}
