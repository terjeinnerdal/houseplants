﻿#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace HousePlants.Domain.Models.Taxonomy
{
    // Family -> Genus -> Species -> (Variety) -> Plants
    // Morocaea -> 
    public abstract class TaxonomyBase
    {
        protected TaxonomyBase(string name, Arcanea arcanea)
        {
            Name = name;
            Arcanea = arcanea;
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Arcanea Arcanea { get; set; }
    }
}