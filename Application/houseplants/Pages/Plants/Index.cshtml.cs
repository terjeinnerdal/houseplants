using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Domain.Models;

namespace HousePlants.Pages.Plants
{
    public class IndexModel : PageModel
    {
        internal class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Plant, PlantDto>(MemberList.Destination);
                CreateMap<PlantDto, Plant>();
            }
        }

        public class PlantDto
        {
            [StringLength(128)] public string LatinName { get; set; }
            [StringLength(128)] public string CommonName { get; set; }
            [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
            public LightRequirement? LightRequirement { get; set; }
            public WaterRequirement? WaterRequirement { get; set; }
            public SoilRequirement? SoilRequirement { get; set; }
            public WateringTechnique? WateringTechnique { get; set; }
            public int? MinimumTemperature { get; set; }
            public int? MaximumTemperature { get; set; }
            public Family Family { get; set; }
            public Genus Genus { get; set; }
            public Species Species { get; set; }
            public LegendStatus? LegendStatus { get; set; }
            internal IEnumerable<string> Tags { get; set; }

            public string TagsString
            {
                get => string.Join(',', Tags);
                set
                {
                    Tags = value != null
                        ? TagsString.Split(',').Select(tag => tag.Trim())
                        : throw new ArgumentNullException(nameof(value));
                }
            }
        }

        private readonly HousePlantsContext _context;
        private readonly IMapper _mapper;

        public IndexModel(HousePlantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<PlantDto> Plants { get; set; }

        public async Task OnGetAsync()
        {
            var domainPlants = await _context.Plants.ToListAsync();
            var test = _mapper.Map<PlantDto>(domainPlants);
        }
    }
}
