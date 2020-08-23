using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Interfaces;

namespace Pet.Game.BackgroundServices
{
    public class CheckPetStatusesService : BackgroundService
    {
        private readonly ILogger<CheckPetStatusesService> logger;
        private IUserRepository userRepository;
        private IPetRepository petRepository;

        private int serviceIntervalInSeconds = 10;

        private IEnumerable<User> users;


        public CheckPetStatusesService(ILogger<CheckPetStatusesService> logger,
            IUserRepository userRepository,
            IPetRepository petRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.petRepository = petRepository;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Update Hunger and Happiness Statusses: {Time}", DateTimeOffset.Now);
                this.users = await userRepository.ListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error trying to get users", ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (this.users != null && this.users.Any())
                {
                    foreach (var user in this.users)
                    {
                        foreach (var pet in user.Pets)
                        {
                            pet.IncreaseHungriness();
                            pet.IncreaseHungriness();
                            await this.petRepository.AddOrUpdateAsync(pet);
                        }
                    }
                }

                await Task.Delay(this.serviceIntervalInSeconds * 1000, stoppingToken);
            }
        }
    }
}
