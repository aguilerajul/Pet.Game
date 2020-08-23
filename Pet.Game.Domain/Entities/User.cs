using Pet.Game.Domain.Base;
using System.Collections.Generic;

namespace Pet.Game.Domain.Entities
{
    public class User : EntityBase
    {
        public virtual IEnumerable<Pet> Pets { get; set; }

        protected User() { }

        private User(string name, IEnumerable<Pet> pets) : base(name)
        {
            this.Pets = pets;
        }
    }
}
