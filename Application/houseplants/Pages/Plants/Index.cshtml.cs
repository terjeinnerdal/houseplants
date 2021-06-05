using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Domain;
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

        private readonly HousePlantsContext _context;
        private readonly IMapper _mapper;

        public IndexModel(HousePlantsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty] public int NumberOfSpecies => _context.Species.Distinct().Count();
        [BindProperty] public int NumberOfPlants => Plants.Count;
        [BindProperty] public IList<PlantDto> Plants { get; set; }

        public async Task OnGetAsync()
        {
            Plants = _mapper.Map<List<PlantDto>>(await _context.Plants.ToListAsync());
        }
    }
}
