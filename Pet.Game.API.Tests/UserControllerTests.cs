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
    public class UserControllerTests : BaseTests
    {       
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<ILogger<UserController>> iloggerMock;

        private readonly UserController userController;
        
        public UserControllerTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            iloggerMock = new Mock<ILogger<UserController>>();

            userController = new UserController(iloggerMock.Object, userRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task Get_User_Async_By_Id_Success()
        {
            var userData = this.mockedData.GetMockedUser("Julio", "Boby", "Dogs");
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(userData));

            // Act
            var result = await userController.Get(this.mockedData.petTypeId);

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
            userRepositoryMock.Setup(repo => repo.ListAsync())
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
            var newUserData = new UserRequestDto {
                Name = "Julio"
            };
            var userData = this.mockedData.GetMockedUserWithoutPet("Julio");

            // Arrange
            userRepositoryMock.Setup(repo => repo.AddOrUpdateAsync(It.IsAny<User>()))
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
    }
}
