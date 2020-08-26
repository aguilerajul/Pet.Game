using Pet.Game.Domain.Entities;
using System;

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

        public Entities.Pet GetMockedPet(string petName, string userName, string petTypeName)
        {
            var mockedPetType = GetMockedPetType(petTypeName);
            var mockedUser = new User(userName);
            var dogPetData = new Entities.Pet(petName, mockedPetType, mockedUser.Id);
            dogPetData.SetId(userId);

            return dogPetData;
        }

        public User GetMockedUserWithoutPet(string name)
        {
            var userData = new User(name);
            userData.SetId(userId);

            return userData;
        }
    }
}
