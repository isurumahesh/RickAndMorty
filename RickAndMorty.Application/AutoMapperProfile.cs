using AutoMapper;
using Newtonsoft.Json;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CharacterDTO, Character>().ForMember(dest => dest.EpisodeUrlsJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Episode))); ;
            CreateMap<OriginDTO, Origin>();
            CreateMap<LocationDTO, Location>();
        }
    }
}