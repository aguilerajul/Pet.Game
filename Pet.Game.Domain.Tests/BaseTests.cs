using Pet.Game.Domain.Tests.Mocks;

namespace Pet.Game.Domain.Tests
{
    public class BaseTests
    {
        public readonly MockedData mockedData;

        public BaseTests()
        {
            mockedData = new MockedData();
        }
    }
}
