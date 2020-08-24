using AutoMapper;
using Pet.Game.API.Profiles;
using Pet.Game.API.Tests.Mocks;

namespace Pet.Game.API.Tests
{
    public class BaseTests
    {
        public readonly IMapper mapper;
        public readonly MockedData mockedData;

        public BaseTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtosProfile());
            });
            mapper = mockMapper.CreateMapper();

            mockedData = new MockedData();
        }
    }
}
