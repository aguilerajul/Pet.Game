using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pet.Game.API.Controllers;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class PetRepositoryTests : BaseTests
    {
        private readonly Mock<IPetRepository> petRepositoryMock;
        private readonly Mock<ILogger<PetController>> iloggerMock;
        private readonly PetController petController;

        public PetRepositoryTests()
        {
            petRepositoryMock = new Mock<IPetRepository>();
            iloggerMock = new Mock<ILogger<PetController>>();
            petController = new PetController(iloggerMock.Object, petRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task Get_Pet_Async_By_Id_Success()
        {
            var dogPetData = this.mockedData.GetMockedPet("Boby", "Julio", "Dogs");

            // Arrange
            petRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(dogPetData));

            // Act
            var result = await petController.Get(this.mockedData.petId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petResponseDtoResult = mapper.Map<PetResponseDto>(dogPetData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petResponseDtoResult);
        }

        [Fact]
        public async Task Get_Pet_Async_By_Id_Not_Found()
        {           
            // Act
            var result = await petController.Get(this.mockedData.petId);

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_Pet_Async_Success()
        {
            var petsData = this.mockedData.GetMockedPets();

            // Arrange
            petRepositoryMock.Setup(repo => repo.ListAsync())
                .Returns(Task.FromResult(petsData));

            // Act
            var result = await petController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petsResponseDtoResult = mapper.Map<IEnumerable<PetResponseDto>>(petsData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petsResponseDtoResult);
        }

        [Fact]
        public async Task List_Pet_Async_By_Id_Not_Found()
        {            
            // Act
            var result = await petController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NoContentResult>();
        }
    }
}
