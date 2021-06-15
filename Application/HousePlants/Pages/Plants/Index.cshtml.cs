using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Models.Plant;

namespace HousePlants.Pages.Plants
{
    public class IndexModel : PageModel
    {
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

        public List<PlantIndexVm> Plants { get; set; }

        public async Task OnGetAsync()
        {
            var plants = await _context.Plants.Include(p => p.Species)
                .ThenInclude(p => p.Genus)
                .ThenInclude(p => p.Family)
                .AsNoTracking()
                .ToListAsync();
            Plants = _mapper.Map<List<PlantIndexVm>>(plants);
            Plants.Sort();
        }

        public class PlantIndexVm : IComparable<PlantIndexVm>, IEquatable<PlantIndexVm>
        {
            [HiddenInput] public Guid Id { get; private set; }
            public string Title { get; set; }
            [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
            public string Family { get; set; }
            public string Genus { get; set; }
            public string Species { get; set; }

            #region IComparable<T>
            public int CompareTo(PlantIndexVm other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                int familyComparison = string.Compare(Family, other.Family, StringComparison.Ordinal);
                if (familyComparison != 0) return familyComparison;
                int genusComparison = string.Compare(Genus, other.Genus, StringComparison.Ordinal);
                if (genusComparison != 0) return genusComparison;
                int titleComparison = string.Compare(Title, other.Title, StringComparison.Ordinal);
                if (titleComparison != 0) return titleComparison;
                int aquiredDateComparison = AquiredDate.CompareTo(other.AquiredDate);
                if (aquiredDateComparison != 0) return aquiredDateComparison;
                return string.Compare(Species, other.Species, StringComparison.Ordinal);
            }

            public static bool operator <(PlantIndexVm left, PlantIndexVm right)
            {
                return Comparer<PlantIndexVm>.Default.Compare(left, right) < 0;
            }

            public static bool operator >(PlantIndexVm left, PlantIndexVm right)
            {
                return Comparer<PlantIndexVm>.Default.Compare(left, right) > 0;
            }

            public static bool operator <=(PlantIndexVm left, PlantIndexVm right)
            {
                return Comparer<PlantIndexVm>.Default.Compare(left, right) <= 0;
            }

            public static bool operator >=(PlantIndexVm left, PlantIndexVm right)
            {
                return Comparer<PlantIndexVm>.Default.Compare(left, right) >= 0;
            }
            #endregion

            #region IEquatable<T>
            public bool Equals(PlantIndexVm other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id.Equals(other.Id) && Title == other.Title && AquiredDate.Equals(other.AquiredDate) && Family == other.Family && Genus == other.Genus && Species == other.Species;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((PlantIndexVm) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Title, AquiredDate, Family, Genus, Species);
            }

            public static bool operator ==(PlantIndexVm left, PlantIndexVm right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PlantIndexVm left, PlantIndexVm right)
            {
                return !Equals(left, right);
            }
            #endregion
        }

        internal class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<Plant, PlantIndexVm>(MemberList.Destination)
                    .ForMember(dest => dest.Species,
                        opt => opt.MapFrom(src => src.Species == null ? string.Empty : src.Species.Name))
                    .ForMember(dest => dest.Genus,
                        opt => opt.MapFrom(src =>
                            src.Species == null || src.Species.Genus == null ? string.Empty : src.Species.Genus.Name))
                    .ForMember(dest => dest.Family,
                        opt => opt.MapFrom(src =>
                            src.Species == null || src.Species.Genus == null || src.Species.Genus.Family == null
                                ? string.Empty
                                : src.Species.Genus.Family.Name));
            }
        }
    }
}
