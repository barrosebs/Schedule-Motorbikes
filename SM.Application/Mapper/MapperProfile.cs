﻿using AutoMapper;
using SM.Domain.Model;
using SM.Domain.Models;
using SM.Domain.ViewModel;


namespace SM.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, InsertUserVM>().ReverseMap();
            CreateMap<BikeModel, BikeVM>().ReverseMap();
            CreateMap<DeliveryPersonModel, DeliveryPersonVM>().ReverseMap();
          
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
