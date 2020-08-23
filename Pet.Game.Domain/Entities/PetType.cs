using Pet.Game.Domain.Base;

namespace Pet.Game.Domain.Entities
{
    public class PetType : EntityBase
    {
        protected PetType() { }
        public PetType(string name) : base(name)
        {

        }
    }
}
