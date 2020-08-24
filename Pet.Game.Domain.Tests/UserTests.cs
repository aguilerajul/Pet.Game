using FluentAssertions;
using Pet.Game.Domain.Tests;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class UserTests : BaseTests
    {
        [Fact]
        public async Task Validate_User_Model()
        {
            var userData = this.mockedData.GetMockedUserWithoutPet("Julio");
            // Assert
            userData.Should().NotBeNull();
            userData.Pets.Should().HaveCount(0);
            userData.Name.Should().Be("Julio");
        }

        [Fact]
        public async Task Add_Pet_User_Model()
        {
            var mockedPet = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");
            var userData = this.mockedData.GetMockedUserWithoutPet("Julio");

            //Act
            userData.AddPet(mockedPet);

            // Assert
            userData.Should().NotBeNull();
            userData.Pets.Count.Should().Be(1);
        }
    }
}
