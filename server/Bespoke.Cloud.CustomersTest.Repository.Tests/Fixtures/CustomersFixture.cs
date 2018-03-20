using AutoMapper;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using System;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.Fixtures
{
    public class CustomersFixture : IDisposable
    {
        public CustomersFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();


                //cfg.CreateMap<Entities.Customer, API.Dtos.CustomerListDto>()
                //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                //        $"{src.FirstName} {src.LastName}"));

                //cfg.CreateMap<Entities.Customer, API.Dtos.CustomerDetailDto>();
            });

        }
        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
/*
 using AutoMapper;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using System;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.Fixtures
{
    public class CustomersFixture : IDisposable
    {
        public CustomersFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Customer, API.Dtos.CustomerListDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                        $"{src.FirstName} {src.LastName}"));

                cfg.CreateMap<Entities.Customer, API.Dtos.CustomerDetailDto>();
            });

        }
        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}

     */
