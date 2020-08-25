using Pet.Game.Domain.Base;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Pet.Game.Domain.Entities
{
    public class User : EntityBase
    {
        private readonly List<Pet> pets = new List<Pet>();
        public virtual IReadOnlyCollection<Pet> Pets => pets.ToList();

        protected User() { }

        public User(string name) : base(name)
        {
            this.pets = Enumerable.Empty<Pet>().ToList();
        }

        public void AddPet(Pet pet)
        {
            var newPet = new Pet(pet.Name, pet.Type, this.Id);
            if (Guid.Empty != pet.Id)
                newPet.SetId(pet.Id);

            this.pets.Add(newPet);
        }
    }
}