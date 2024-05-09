﻿using AutoMapper;
using SM.Domain.Models;
using SM.Domain.ViewModel;


namespace SM.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, InsertUserVM>().ReverseMap();
            CreateMap<Bike, BikeVM>().ReverseMap();
          
            //exemplo
            //CreateMap<Group, GroupViewModel>().ReverseMap()
            //    .ForMember(dest => dest.Description, src => src.Ignore())
            //    .AfterMap((src, dest) =>
            //    {
            //        dest.CreatedAt = DateTime.Now;
            //        dest.Description = dest.Name;
            //    });
        }
    }
}
