using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;

#nullable enable
namespace HousePlants.Models.Interfaces
{
    public abstract class HousePlantsEntityBase : IUserOwnedEntity<string>
    {
        [Key, HiddenInput, MaxLength(128)] 
        public Guid Id { get; set; }

        [StringLength(128)] 
        public string Title { get; } = string.Empty!;

        [StringLength(20000), DataType(DataType.MultilineText)] 
        public string? Description { get; set; }

        [HiddenInput, ScaffoldColumn(false)] 
        public Instant Created { get; set; }
        
        [HiddenInput, ScaffoldColumn(false)] 
        public Instant Modified { get; set; }
        public string Owner { get; set; }

        public HousePlantsEntityBase(string owner)
        {
            Owner = owner;
        }
    }
}