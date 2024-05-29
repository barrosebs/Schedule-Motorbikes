using AutoMapper;
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
            CreateMap<UserModel, UserDeliveryPersonVM>().ReverseMap();
            CreateMap<UserModel, ResetPasswordVM>().ReverseMap();
            CreateMap<DeliveryPersonVM, UserDeliveryPersonVM>().ReverseMap();
            CreateMap<DeliveryPersonModel, DeliveryPersonVM>().ReverseMap();
            CreateMap<MotorcycleModel, MotorcycleVM>().ReverseMap();
            CreateMap<AllocationModel, AllocationVM>().ReverseMap();
          
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
