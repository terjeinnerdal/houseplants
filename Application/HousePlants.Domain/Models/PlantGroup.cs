using System.Collections.Generic;

namespace HousePlants.Domain.Models
{
    public class PlantGroup : BaseEntity
    {
        public ICollection<Plant> Plants { get; set; } = new List<Plant>();
    }
}