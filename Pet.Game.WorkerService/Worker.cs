using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Enums;
using Pet.Game.Domain.Interfaces;

namespace Pet.Game.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        private IConfiguration configuration;
        private IUserRepository userRepository;
        private IPetRepository petRepository;        

        private int intervalInSeconds = 60;
        private IEnumerable<User> users;

        public Worker(ILogger<Worker> logger,
            IConfiguration configuration,
            IUserRepository userRepository,
            IPetRepository petRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.petRepository = petRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Update Hunger and Happiness Statusses: {Time}", DateTimeOffset.Now);
            intervalInSeconds = int.Parse(this.configuration.GetSection("IntervalInSeconds").Value);
            logger.LogInformation("Interval: {Interval}", intervalInSeconds);

            this.users = await userRepository.ListAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateStatusess();
                }
                catch (Exception ex)
                {
                    logger.LogError("Error trying to get users", ex);
                }

                await Task.Delay(this.intervalInSeconds * 1000, stoppingToken);
            }
        }

        private async Task UpdateStatusess()
        {
            if (this.users != null && this.users.Any())
            {
                foreach (var user in this.users)
                {
                    foreach (var pet in user.Pets)
                    {
                        pet.Increase<HungrinessStatus>(pet.Type.HungrinessInterval);
                        pet.Decrease<HappinessStatus>(pet.Type.HappinessInterval);

                        await this.petRepository.AddOrUpdateAsync(pet);
                        logger.LogInformation("Pet Updated: {Pet}", pet);
                    }
                }
            }
        }
    }
}
