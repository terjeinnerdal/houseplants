using AutoMapper;
using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Pages.Plants;

namespace HousePlants.Infrastructure.MapperProfiles
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Plant, PlantDto>(MemberList.Destination);
            CreateMap<PlantDto, Plant>();
        }
    }

}
