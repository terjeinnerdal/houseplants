using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

namespace HousePlants.Domain.Models
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
        public string? Title { get; set; }

        [DataType(DataType.MultilineText), StringLength(20000)]
        public string? Description { get; set; }
    }

    public class Division : BaseEntity { }
    public class Klasse : BaseEntity { }
    public class Order : BaseEntity { }
    public class Family : BaseEntity { }
    public class Genus : BaseEntity { }
    public class Species : BaseEntity { }
    public class Variety : BaseEntity { }
}