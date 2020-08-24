using Pet.Game.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pet.Game.Domain.Tests.Mocks
{
    public class MockedData
    {
        public readonly Guid petTypeId;
        public readonly Guid userId;
        public readonly Guid petId;

        public MockedData()
        {
            petTypeId = Guid.Parse("bbef09aa-4617-4ce2-844c-d9d78a5a1c63");
            petId = Guid.Parse("68c6698e-8061-420c-8b90-a38c52bf56de");
            userId = Guid.Parse("326efe0e-e91b-4213-a6e8-c3f16813c6e6");
        }

        public PetType GetMockedPetType(string name)
        {
            var dogPetTytpeData = new PetType(name, 1, 1);
            dogPetTytpeData.SetId(petTypeId);

            return dogPetTytpeData;
        }

        public Domain.Entities.Pet GetMockedPet(string petName, string userName, string petTypeName)
        {
            var mockedPetType = GetMockedPetType(petTypeName);
            var mockedUser = new User(userName, Enumerable.Empty<Domain.Entities.Pet>());
            var dogPetData = new Domain.Entities.Pet(petName, mockedPetType, mockedUser);
            dogPetData.SetId(userId);

            return dogPetData;
        }

        public User GetMockedUser(string userName)
        {
            var userData = new User(userName, Enumerable.Empty<Domain.Entities.Pet>());
            userData.SetId(userId);
            userData.AddPet(GetMockedPet("Pet", userName, "PetType"));

            return userData;
        }

        public User GetMockedUserWithoutPet(string name)
        {
            var userData = new User(name, Enumerable.Empty<Domain.Entities.Pet>());
            userData.SetId(userId);

            return userData;
        }

        public IEnumerable<User> GetMockedUsers()
        {
            return new List<User>
            {
                GetMockedUser("Julio")
            };
        }

        public IEnumerable<Domain.Entities.Pet> GetMockedPets()
        {
            return new List<Domain.Entities.Pet>
            {
                GetMockedPet("Boby", "Julio", "Dogs")
            };
        }

        public IEnumerable<PetType> GetMockedPetTypes()
        {
            return new List<PetType>
            {
                new PetType("Dogs", 1, 1),
                new PetType("Cats", 1, 1),
                new PetType("Reptiles", 1, 1),
                new PetType("Birds", 1, 1)
            };
        }

    }
}
