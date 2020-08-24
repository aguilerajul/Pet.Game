using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;
using Pet.Game.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.API.Tests
{
    public class UserRepositoryTests : BaseTests
    {
        private readonly Mock<IConfiguration> configurationMock;        
        private readonly Mock<IPetRepository> petRepositoryMock;
        private readonly IUserRepository userRepository;

        public UserRepositoryTests()
        {
            petRepositoryMock = new Mock<IPetRepository>();
            userRepository = new UserRepository(configurationMock.Object, petRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_User_Async_By_Id_Success()
        {
            var userData = this.mockedData.GetMockedUser("Julio", "Boby", "Dogs");
            // Arrange
            //userRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
            //    .Returns(Task.FromResult(userData));

            // Act
            var userResult = await userRepository.GetAsync()

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var userResponseDtoResult = mapper.Map<UserResponseDto>(userData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(userResponseDtoResult);
        }

        [Fact]
        public async Task Get_User_Async_By_Id_Not_Found()
        {
            // Act
            var result = await userController.Get(this.mockedData.userId);

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task List_User_Async_Success()
        {
            var users = this.mockedData.GetMockedUsers();

            // Arrange
            userRepository.Setup(repo => repo.ListAsync())
                .Returns(Task.FromResult(users));

            // Act
            var result = await userController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var petTypesDtoResult = mapper.Map<IEnumerable<UserResponseDto>>(users);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(petTypesDtoResult);
        }

        [Fact]
        public async Task List_User_Async_Not_Found()
        {
            // Act
            var result = await userController.List();

            // Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Add_User_Async_Success()
        {
            var newUserData = new UserRequestDto
            {
                Name = "Julio"
            };
            var userData = this.mockedData.GetMockedUserWithoutPet("Julio");

            // Arrange
            userRepository.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(userData));

            // Act
            var result = await userController.Post(newUserData);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var userResponseDto = mapper.Map<UserResponseDto>(userData);
            result.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(userResponseDto);
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
            userRepository.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult(userData));

            userRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(userData));

            petTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(petTypeData));

            petRepositoryMock.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<Domain.Entities.Pet>()))
                .Returns(Task.FromResult(petData));

            // Act
            var result = await userController.AddPet(petRequestDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var userResponseDto = mapper.Map<UserResponseDto>(userData);
        }
    }
}
