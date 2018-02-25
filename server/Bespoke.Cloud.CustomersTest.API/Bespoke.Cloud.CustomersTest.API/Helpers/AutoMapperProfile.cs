using AutoMapper;

namespace Bespoke.Cloud.CustomersTest.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Entities.Customer, Models.CustomerListDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                $"{src.FirstName} {src.LastName}"));
        }
    }
}
