using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pet.Game.WorkerService.Tests
{
    public class WorkerTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IPetRepository> petRepositoryMock;
        private readonly Mock<ILogger<Worker>> loggerWorkerMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly Worker worker;

        public WorkerTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            petRepositoryMock = new Mock<IPetRepository>();
            loggerWorkerMock = new Mock<ILogger<Worker>>();
            configurationMock = new Mock<IConfiguration>();

            worker = new Worker(loggerWorkerMock.Object, configurationMock.Object, userRepositoryMock.Object, petRepositoryMock.Object);
        }

        [Fact]
        public void Worker_Execute_Async_Users_Pet_Status_Update()
        {
            var user = new User("Julio");
            user.AddPet(new Domain.Entities.Pet("Boby", new PetType("Dogs", 1, 1), user.Id));
            IEnumerable<User> users = new List<User> { 
                user
            };

            //Act
            userRepositoryMock.Setup(ur => ur.ListAsync())
                .Returns(Task.FromResult(users));

            configurationMock.Setup(c => c.GetSection(It.IsAny<string>()).Value).Returns("60");

            worker.StartAsync(new System.Threading.CancellationToken());
        }
    }
}
