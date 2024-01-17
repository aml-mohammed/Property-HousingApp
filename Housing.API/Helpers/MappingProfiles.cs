using AutoMapper;
using Housing.API.DTOS;
using Housing.API.Models;

namespace Housing.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Property, PropertyListDto>()
                .ForMember(p=>p.City,opt=>opt.MapFrom(src=>src.City.Name))
                .ForMember(p =>p.Country ,opt => opt.MapFrom(src => src.City.Country))
                .ForMember(p => p.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(p => p.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name)).
                ForMember(p=>p.photo,opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsPrimary).ImageUrl));

            CreateMap<Property, PropertyDetailDto>()
               .ForMember(p => p.City, opt => opt.MapFrom(src => src.City.Name))
               .ForMember(p => p.Country, opt => opt.MapFrom(src => src.City.Country))
               .ForMember(p => p.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
               .ForMember(p => p.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<PropertyType, KeyValuePairDto>().ReverseMap();
            CreateMap<FurnishingType, KeyValuePairDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();

        }
    }
}
