using AutoMapper;
using ParkAPI_12.Models;
using ParkAPI_12.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_12.DTO_Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
        }
    }
}
