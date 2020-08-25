using Microsoft.EntityFrameworkCore;
using Pet.Game.Domain.Entities;
using System;

namespace Pet.Game.Repository.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            GeneratePetTypes(modelBuilder);
        }

        private static void GeneratePetTypes(ModelBuilder modelBuilder)
        {
            var petTypeNames = new[] { "Cats", "Dogs", "Birds", "Reptiles" };

            foreach (var petType in petTypeNames)
            {
                var newPetType = new PetType(petType, 1, 1);
                newPetType.SetId(Guid.NewGuid());
                modelBuilder.Entity<PetType>().HasData(newPetType);
            }
        }
    }
}
