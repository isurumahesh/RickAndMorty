using AutoMapper;
using Newtonsoft.Json;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;

namespace RickAndMorty.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CharacterDTO, Character>()
                .ForMember(dest => dest.EpisodeUrlsJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Episode)))
                .ReverseMap()
                .ForMember(dest => dest.Episode, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.EpisodeUrlsJson)));

            CreateMap<CharacterSaveDTO, Character>();
            CreateMap<LocationDTO, Location>().ReverseMap();
        }
    }
}