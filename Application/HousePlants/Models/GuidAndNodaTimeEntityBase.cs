using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

#nullable enable
namespace HousePlants.Models
{
    public abstract class GuidAndNodaTimeEntityBase
    {
        [Key, HiddenInput]
        public Guid Id { get; private set; }

        [HiddenInput, ScaffoldColumn(false)]
        public Instant Created { get; internal set; }

        [HiddenInput, ScaffoldColumn(false)]
        public Instant Modified { get; internal set; }

        [StringLength(128)] 
        public virtual string Title { get; } = default!;

        [StringLength(20000), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
    }
}