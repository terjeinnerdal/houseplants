using System.Collections.Generic;

namespace HousePlants.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Plant.Plant> Plants { get; set; } = new List<Plant.Plant>();

        public Tag(string name)
        {
            Name = name;
        }
    }
}