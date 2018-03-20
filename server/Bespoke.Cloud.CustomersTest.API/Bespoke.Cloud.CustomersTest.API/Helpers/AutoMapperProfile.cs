using AutoMapper;

namespace Bespoke.Cloud.CustomersTest.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Entities.Customer, Dtos.CustomerListDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                $"{src.FirstName} {src.LastName}"));

            CreateMap<Entities.Customer, Dtos.CustomerDetailDto>();

            CreateMap<Entities.User, Dtos.UserDisplayDto>();

            CreateMap<Entities.User, Dtos.UserDto>();
            CreateMap<Dtos.UserDto, Entities.User>();
        }
    }
}
