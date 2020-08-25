using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Pet.Game.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class PetTypeRepositoryTests : BaseTests
    {       
        private readonly Mock<IPetTypeRepository> petTypeRepositoryMock;
       
        public PetTypeRepositoryTests()
        {
            petTypeRepositoryMock = new Mock<IPetTypeRepository>();
        }

        [Fact]
        public async Task Get_PetType_Async_By_Id_Success()
        {
            var petTypeData = this.mockedData.GetMockedPetType("Cats");
            // Arrange
            petTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(petTypeData));

            // Act
            var result = await petTypeController.Get(this.mockedData.petTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<PetTypeDto>>();
            var petTypeDtoResult = mapper.Map<PetTypeDto>(petTypeData);
            result.Result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petTypeDtoResult);
        }

        [Fact]
        public async Task Get_PetType_Async_By_Id_Not_Found()
        {
            // Act
            var result = await petTypeController.Get(this.mockedData.petTypeId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<PetTypeDto>>();
            result.Result.Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_PetType_Async_Success()
        {
            var petTypesData = this.mockedData.GetMockedPetTypes();

            // Arrange
            petTypeRepositoryMock.Setup(repo => repo.ListAsync())
                .Returns(Task.FromResult(petTypesData));

            // Act
            var result = await petTypeController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petTypesDtoResult = mapper.Map<IEnumerable<PetTypeDto>>(petTypesData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petTypesDtoResult);
        }

        [Fact]
        public async Task List_PetType_Async_Not_Found()
        {
            // Act
            var result = await petTypeController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Add_PetType_Async_Success()
        {
            var petTypeData = this.mockedData.GetMockedPetType("Cats");
            // Arrange
            petTypeRepositoryMock.Setup(repo => repo.AddOrUpdateAsync(petTypeData))
                .Returns(Task.FromResult(petTypeData));

            // Act
            var result = await petTypeController.Post(mapper.Map<PetTypeDto>(petTypeData));

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petTypeDtoResult = mapper.Map<PetTypeDto>(petTypeData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petTypeDtoResult);
        }
    }
}
