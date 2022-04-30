
using AutoMapper;
using GreatWorld.Models;
using GreatWorld.ViewModel;

namespace GreatWorld.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Create map to concatenate FirstName and LastName
            CreateMap<TripViewModel, Trip>()
              .ForMember(dest => dest.FullName,
                            src => src.MapFrom(src => src.FirstName + " " + src.LastName)
                            );

            //Create map to fetch FirstName and LastName
            CreateMap<Trip, TripViewModel>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom<FirstNameResolver>())
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom<LastNameResolver>());

            //Create map between Stop and StopViewModel
            CreateMap<Stop, StopViewModel>().ReverseMap();

        }
    }
}
