using FluentAssertions;
using Pet.Game.Domain.Tests;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class PetTypeTests : BaseTests
    {
        [Fact]
        public async Task Validate_PetType_Model()
        {
            var userData = this.mockedData.GetMockedPetType("Dogs");
            // Assert
            userData.Should().NotBeNull();
            userData.Name.Should().Be("Dogs");
        }
    }
}
