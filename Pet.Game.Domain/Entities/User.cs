using Pet.Game.Domain.Base;
using System.Linq;
using System.Collections.Generic;

namespace Pet.Game.Domain.Entities
{
    public class User : EntityBase
    {
        private readonly List<Pet> pets = new List<Pet>();
        public virtual IReadOnlyCollection<Pet> Pets => pets.ToList();

        protected User() { }

        public User(string name, IEnumerable<Pet> pets) : base(name)
        {
            this.pets = (pets ?? Enumerable.Empty<Pet>()).ToList();
        }
        public void AddPet(Pet pet)
        {
            this.pets.Add(new Pet(pet.Name, pet.Type, pet.HappinessDecreaseInterval, pet.HungrinessIncreaseInterval));
        }
    }
}