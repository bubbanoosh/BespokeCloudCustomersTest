using AutoMapper;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using System;

namespace Bespoke.Cloud.CustomersTest.Repository.Tests.Fixtures
{
    public class UsersFixture : IDisposable
    {
        public UsersFixture()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();

                //cfg.CreateMap<Entities.User, API.Dtos.UserDisplayDto>();

                //cfg.CreateMap<Entities.User, API.Dtos.UserDto>();
                //cfg.CreateMap<API.Dtos.UserDto, Entities.User>();
            });

        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
