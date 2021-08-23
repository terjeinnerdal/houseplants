using System;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace HousePlants.Models.Plant
{
    public enum NutrientRequirement
    {
        Low,
        Medium,
        High
    }
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
    public abstract class LogEntry
    {
        protected abstract LogEntryTask GetTask();
        public LogEntryTask? Task => GetTask();
        public LocalDateTime When { get; set; }
        [ForeignKey(nameof(Plant))]
        public Guid PlantId { get; set; }
        public Plant Plant { get; set; } = default!;
    }

    public sealed class Fertigation : LogEntry
    {
        protected override LogEntryTask GetTask() => LogEntryTask.Watering;
        public float LitersOfWater { get; set; }
    }
}