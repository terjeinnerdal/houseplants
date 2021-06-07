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
using HousePlants.Models;

namespace HousePlants.Pages.Plants
{
    public class IndexModel : PageModel
    {
        internal class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Plant, PlantIndexVm>(MemberList.Destination);
            }
        }

        public class PlantIndexVm
        {
            [HiddenInput]
            public Guid Id { get; private set; }
            public string Title { get; set; }
            [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
        }
        private readonly HousePlantsContext _context;
        private readonly IMapper _mapper;

        public IndexModel(HousePlantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int NumberOfFamilies => _context.Families.Distinct().Count();
        public int NumberOfGenuses => _context.Genus.Distinct().Count();
        public int NumberOfSpecies => _context.Species.Distinct().Count();
        public int NumberOfPlants => Plants.Count;
        [BindProperty] public IList<PlantIndexVm> Plants { get; set; }

        public async Task OnGetAsync()
        {
            Plants = _mapper.Map<List<PlantIndexVm>>(
                await _context.Plants
                    .OrderBy(p => p.Species.Genus.Name)
                    .ToListAsync());
        }
    }
}
