using FluentAssertions;
using Pet.Game.Domain.Enums;
using Pet.Game.Domain.Tests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class PetTests : BaseTests
    {
        [Theory]
        [InlineData("Pet1", "Julio", "Dogs")]
        [InlineData("Pet2", "Juan", "Cats")]
        public async Task Validate_Pet_Model(string petName, string userName, string petTypeName)
        {
            var petData = this.mockedData.GetMockedPet(petName, userName, petTypeName);
            var petTypeData = this.mockedData.GetMockedPetType(petTypeName);

            // Assert
            petData.Should().NotBeNull();
            petData.Name.Should().Be(petName);
            petData.HappinessStatus.Should().Be(Domain.Enums.HappinessStatus.Neutral);
            petData.HungrinessStatus.Should().Be(Domain.Enums.HungrinessStatus.Neutral);
            petData.Type.Name.Should().BeEquivalentTo(petTypeData.Name);
        }

        [Theory]
        [InlineData(HappinessStatus.Neutral)]
        [InlineData(HungrinessStatus.Neutral)]
        public async Task Pet_Increase_Successfull<T>(T status)
        {
            var mockedPet = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");
            var previousHappinessStatus = mockedPet.HappinessStatus;
            var previousHungrinessStatus = mockedPet.HungrinessStatus;

            //Act
            mockedPet.Increase<T>(1);

            // Assert
            mockedPet.Should().NotBeNull();

            var statusType = typeof(T);
            if (statusType == typeof(HungrinessStatus))
            {
                mockedPet.HungrinessStatus.Should().Be(HungrinessStatus.LittleHunger);
            }
            else if (statusType == typeof(HappinessStatus))
            {
                mockedPet.HappinessStatus.Should().Be(HappinessStatus.Nice);
            }
        }

        [Theory]
        [InlineData(HappinessStatus.Neutral)]
        [InlineData(HungrinessStatus.Neutral)]
        public async Task Pet_Decrease_Successfull<T>(T status)
        {
            var mockedPet = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");

            //Act
            mockedPet.Decrease<T>(1);

            // Assert
            mockedPet.Should().NotBeNull();

            var statusType = typeof(T);
            if (statusType == typeof(HungrinessStatus))
            {
                mockedPet.HungrinessStatus.Should().Be(HungrinessStatus.Nice);
            }
            else if (statusType == typeof(HappinessStatus))
            {
                mockedPet.HappinessStatus.Should().Be(HappinessStatus.Normal);
            }
        }

        [Fact]
        public void Stroke_Pet()
        {
            var mockedPet = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");
            var previousHappinessStatus = mockedPet.HappinessStatus;

            //Act
            mockedPet.Stroke(1);

            // Assert
            mockedPet.Should().NotBeNull();
            mockedPet.HappinessStatus.Should().Be(previousHappinessStatus + 1);
        }

        [Fact]
        public void Feed_Pet()
        {
            var mockedPet = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");
            var previousHungrinessStatus = mockedPet.HungrinessStatus;

            //Act
            mockedPet.Feed(1);

            // Assert
            mockedPet.Should().NotBeNull();
            mockedPet.HungrinessStatus.Should().Be(previousHungrinessStatus - 1);
        }
    }
}
