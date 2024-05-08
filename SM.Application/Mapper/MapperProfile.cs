using AutoMapper;


namespace SM.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
          //  CreateMap<Empresa, EmpresaVM>().ReverseMap();
          
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
