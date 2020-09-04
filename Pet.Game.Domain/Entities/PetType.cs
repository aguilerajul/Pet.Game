using Pet.Game.Domain.Base;

namespace Pet.Game.Domain.Entities
{
    public class PetType : EntityBase
    {
        public int HappinessInterval { get; set; }
        public int HungrinessInterval { get; set; }

        protected PetType() { }
        public PetType(string name, int happinessInterval, int hungrinessInterval) : base(name)
        {
            this.HappinessInterval = happinessInterval;
            this.HungrinessInterval = hungrinessInterval;
        }
    }
}
