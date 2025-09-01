using Domain.Entities;
using Application.DTOs.URLToken;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<URlToken, URLTokenDto>().ReverseMap();
            CreateMap<URlToken, CreateURLTokenDto>().ReverseMap();
            CreateMap<URlToken, UpdateURLTokenDto>().ReverseMap();
        }
    }
}
