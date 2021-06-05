using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace HousePlants.Domain
{
    /// <summary>
    /// BaseEntity using a Guid for the Id and NodaTime types for the Created and Modified timestamps.
    /// </summary>
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Created = SystemClock.Instance.GetCurrentInstant();
        }

        [HiddenInput, Key]
        public Guid Id { get; private set; }

        [HiddenInput, ScaffoldColumn(false)]
        public Instant Created { get; set; }

        [HiddenInput, ScaffoldColumn(false)]
        public Instant? Modified { get; set; }

        [StringLength(128)]
        public abstract string Title { get; set; }

        [DataType(DataType.MultilineText), StringLength(20000)]
        public string? Description { get; set; }
    }
}