using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HousePlants.Models.Plant.Taxonomy;
using NodaTime;

#nullable enable
namespace HousePlants.Models.Plant
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public byte[] Data { get; set; }

        public UploadedFile(Guid id, byte[] data)
        {
            Id = id;
            Data = data;
        }
    }

    public enum NutrientRequirement
    {
        Low,
        Medium,
        High
    }

    // User owned entity
    public class Plant : GuidAndNodaTimeEntityBase, IComparable<Plant>
    {
        public enum LogEntryTask
        {
            GeneralLogEntry,
            Cloning,
            Transplanting,
            Trimming,
            Watering,
            Defoliating,
            LowStressTraining,
            Pruning
        }

        public abstract class LogEntryAndNodaTimeEntityBase : GuidAndNodaTimeEntityBase
        {
            protected abstract LogEntryTask GetTask();
            public LogEntryTask? Task => GetTask();
            public LocalDateTime When { get; set; }
            [ForeignKey(nameof(Plant))]
            public Guid PlantId { get; set; }
            public Plant Plant { get; set; } = default!;
        }
        
        public sealed class Fertigation : LogEntryAndNodaTimeEntityBase
        {
            protected override LogEntryTask GetTask() => LogEntryTask.Watering;
            public float LitersOfWater { get; set; }
        }


        // This is Species, most people proably just want to write ficus lyrata.
        [StringLength(128)] public string? LatinName => Species?.Name;
        [StringLength(128)] public string? CommonName { get; set; }
        public DateTime? AquiredDate { get; set; }
        public int MinimumTemperature { get; set; }

        public Species? Species { get; set; }
        public IList<Tag>? Tags { get; set; } = new List<Tag>();
        public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();

        /// <summary>
        /// Returns "CommonName - LatinName" where the dash is only included if both are present. If only CommonName or LatinName is set then that is returned as is.
        /// </summary>
        public override string Title
        {
            get
            {
                string title = string.Empty;
                if (!string.IsNullOrEmpty(CommonName) && !string.IsNullOrEmpty(LatinName))
                {
                    title = $"{CommonName} - {LatinName}";
                }
                else if(!string.IsNullOrEmpty(LatinName))
                {
                    title = LatinName;
                }
                else if (!string.IsNullOrEmpty(CommonName))
                {
                    title = CommonName;
                }

                return title;
            }
        }

        public int CompareTo(Plant? other)
        {
            return string.Compare(Title, other?.Title, StringComparison.Ordinal);
        }
    }
}
