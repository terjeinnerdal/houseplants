#nullable enable
using System;
using HousePlants.Models.Interfaces;

namespace HousePlants.Models.Plant.Taxonomy
{
    public abstract class TaxonomyBase
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected TaxonomyBase(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}