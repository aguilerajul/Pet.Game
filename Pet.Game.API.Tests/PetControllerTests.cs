using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pet.Game.API.Controllers;
using Pet.Game.API.Dtos;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class PetControllerTests : BaseTests
    {
        private readonly Mock<IPetRepository> petRepositoryMock;
        private readonly Mock<ILogger<PetController>> iloggerMock;
        private readonly PetController petController;
        private readonly Mock<IPetTypeRepository> petTypeRepositoryMock;
        private readonly Mock<IUserRepository> userRepositoryMock;

        public PetControllerTests()
        {
            petRepositoryMock = new Mock<IPetRepository>();
            iloggerMock = new Mock<ILogger<PetController>>();
            petTypeRepositoryMock = new Mock<IPetTypeRepository>();
            userRepositoryMock = new Mock<IUserRepository>();

            petController = new PetController(iloggerMock.Object, userRepositoryMock.Object, petRepositoryMock.Object, petTypeRepositoryMock.Object, mapper);
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

        [Fact]
        public async Task Add_User_Pet_Async_Success()
        {
            var petName = "Boby";
            var userName = "Julio";
            var petTypeName = "Dogs";
            var petRequestDto = new PetRequestDto
            {
                Name = petName,
                PetId = this.mockedData.petId,
                TypeId = this.mockedData.petTypeId,
                UserId = this.mockedData.userId
            };

            var userData = this.mockedData.GetMockedUser(userName, petName, petTypeName);
            var petTypeData = this.mockedData.GetMockedPetType(petTypeName);
            var petData = this.mockedData.GetMockedPet(petName, userName, petTypeName);

            // Arrange
            userRepositoryMock.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(userData));

            userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(userData));

            petTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(petTypeData));

            petRepositoryMock.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<Domain.Entities.Pet>()))
                .Returns(Task.FromResult(petData));

            // Act
            var result = await petController.AddPet(petRequestDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Stroke_User_Pet_Async_Success()
        {
            var petName = "Boby";
            var userName = "Julio";
            var petTypeName = "Dogs";
            var petData = this.mockedData.GetMockedPet(petName, userName, petTypeName);

            // Arrange
            petRepositoryMock.Setup(repo => repo.Stroke(It.IsAny<Guid>()))
                .Returns(Task.FromResult(petData));

            // Act
            var result = await petController.StrokePet(this.mockedData.petId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petResponseDto = mapper.Map<PetResponseDto>(petData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petResponseDto);
        }

        [Fact]
        public async Task Feed_User_Pet_Async_Success()
        {
            var petName = "Boby";
            var userName = "Julio";
            var petTypeName = "Dogs";
            var petData = this.mockedData.GetMockedPet(petName, userName, petTypeName);

            // Arrange
            petRepositoryMock.Setup(repo => repo.Feed(It.IsAny<Guid>()))
                .Returns(Task.FromResult(petData));

            // Act
            var result = await petController.FeedPet(this.mockedData.petId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petResponseDto = mapper.Map<PetResponseDto>(petData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petResponseDto);
        }
    }
}
