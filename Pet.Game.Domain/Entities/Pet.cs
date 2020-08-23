using Pet.Game.Domain.Base;
using Pet.Game.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.Domain.Entities
{
    public class Pet : EntityBase
    {
        public HungerStatus HungerStatus { get; set; }
        public HappinessStatus HappinessStatus { get; set; }

        [Required]
        public PetType Type { get; set; }

        protected Pet() {}

        public Pet(string name, PetType petType, HungerStatus hungerStatus = HungerStatus.Neutral, HappinessStatus happinessStatus = HappinessStatus.Neutral) : base(name)
        {
            this.Type = petType;
            this.HungerStatus = hungerStatus;
            this.HappinessStatus = happinessStatus;
        }
    }
}
