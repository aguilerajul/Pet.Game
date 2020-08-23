using System;
using System.Linq;
using Pet.Game.Domain.Base;
using Pet.Game.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.Domain.Entities
{
    public class Pet : EntityBase
    {
        public HungrinessStatus HungrinessStatus { get; private set; }
        public HappinessStatus HappinessStatus { get; private set; }

        public int HappinessDecreaseInterval { get; private set; }

        public int HungrinessIncreaseInterval { get; private set; }

        [Required]
        public virtual PetType Type { get; private set; }

        protected Pet() {}

        public Pet(string name, 
            PetType petType,
            int happinessDecreaseInterval = 1,
            int hungrinessIncreaseInterval = 1) : base(name)
        {
            this.Type = petType;
            this.HungrinessStatus = HungrinessStatus.Neutral;
            this.HappinessStatus = HappinessStatus.Neutral;
            this.HappinessDecreaseInterval = happinessDecreaseInterval;
            this.HungrinessIncreaseInterval = hungrinessIncreaseInterval;
        }

        public void IncreaseHungriness()
        {
            var maxHungerStatus = Enum.GetValues(typeof(HungrinessStatus)).Cast<HungrinessStatus>().Max();
            var nextStatus = this.HungrinessStatus + this.HungrinessIncreaseInterval;
            if (!nextStatus.Equals(maxHungerStatus))
            {
                this.HungrinessStatus = nextStatus;
                this.LastModified = DateTime.UtcNow;
            }                
        }
        public void DecreaseHappiness()
        {
            var minHapinnessStatus = Enum.GetValues(typeof(HappinessStatus)).Cast<HappinessStatus>().Min();
            var nextStatus = this.HappinessStatus - this.HappinessDecreaseInterval;
            if (!nextStatus.Equals(minHapinnessStatus))
            {
                this.HappinessStatus = nextStatus;
                this.LastModified = DateTime.UtcNow;
            }
        }
    }
}
