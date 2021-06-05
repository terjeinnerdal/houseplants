using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using HousePlants.Domain.Models.Requirements;
using HousePlants.Domain.Models.Taxonomy;
using NodaTime;

#nullable enable
namespace HousePlants.Domain
{
    public enum Month
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    // todo: this should be an owned entity type. But is it owned by Plant or Species. I would say species at first glance.

    public class PlantPassport
    {

        /// <summary>
        /// Contains start and end month for when a plant produces flowers.
        /// This class does not need a primary key. Is it a owned entity?
        /// </summary>

        public Guid Id { get; set; }

        public bool Perennial { get; set; }

        public FloweringPeriod? FloweringPeriod { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public WaterRequirement WaterRequirement { get; set; }
        public NutrientRequirement NutrientRequirement { get; set; }
        public int HeightInCentimeters { get; set; }
        public bool Eadible { get; set; }
        //public bool Toxic { get; set; }
        
        public List<Plant> Plants { get; set; } = new List<Plant>();
    }

    public enum NutrientRequirement
    {
        Low,
        Medium,
        High
    }

    public class Plant : BaseEntity
    {
        [Flags]
        public enum Legends
        {
            None,
            Survivor = 1,
            OldBoy = 2,
            Neglected = 4,
            LowMaint = 8,
            HighMaint = 16,
            FamilyMember = 32
        }

        // This is Species, most people proably just want to write ficus lyrata.
        [StringLength(128)] public string? LatinName { get; set; }
        [StringLength(128)] public string? CommonName { get; set; }
        public DateTime? AquiredDate { get; set; }
        public int MinimumTemperature { get; set; }
        public int MaximumTemperature { get; set; }
        public LightRequirement? LightRequirement { get; set; }
        public WaterRequirement? WaterRequirement { get; set; }
        public SoilRequirement? SoilRequirement { get; set; }
        public WateringTechnique WateringTechnique { get; set; }
        public Legends? Status { get; set; }

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
            set { _ = value; }
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
