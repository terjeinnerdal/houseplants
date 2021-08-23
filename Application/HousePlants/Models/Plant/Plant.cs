using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HousePlants.Models.Interfaces;
using HousePlants.Models.Plant.Taxonomy;
using NodaTime;

#nullable enable
namespace HousePlants.Models.Plant
{

    // User owned entity
    public class Plant : IGuidEntity, ITitledEntity, IDescibedEntity, IComparable<Plant>, IUserOwnedEntity<Guid>
    {
        public Guid Id { get; private set; }
        public Guid Owner { get; set; }
        public string Description { get; set; }
        
        [StringLength(128)] public string? LatinName { get; set; }
        [StringLength(128)] public string? CommonName { get; set; }
        public DateTime? AquiredDate { get; set; }
        public int MinimumTemperature { get; set; }
        public Species? Species { get; set; }
        public IList<Tag>? Tags { get; set; } = new List<Tag>();
        public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();

        /// <summary>
        /// Returns "CommonName - LatinName" where the dash is only included if both are present. If only CommonName or LatinName is set then that is returned as is.
        /// </summary>
        public string Title
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
