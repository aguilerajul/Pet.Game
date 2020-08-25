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

        public Guid UserId { get; private set; }

        [Required]
        public virtual PetType Type { get; private set; }

        protected Pet() {}

        public Pet(string name, 
            PetType petType, Guid userId) : base(name)
        {
            this.Type = petType;
            this.HungrinessStatus = HungrinessStatus.Neutral;
            this.HappinessStatus = HappinessStatus.Neutral;
            this.UserId = userId;
        }

        public void Increase<T>(int interval)
        {
            var type = typeof(T);
            var maxStatusValue = Enum.GetValues(type).Cast<T>().Max();
            if (type == typeof(HungrinessStatus))
            {
                var nextStatus = this.HungrinessStatus + interval;
                if (!nextStatus.Equals(maxStatusValue))
                {
                    this.HungrinessStatus = nextStatus;
                    this.LastModified = DateTime.UtcNow;
                }
            }
            else if (type == typeof(HappinessStatus))
            {
                var nextStatus = this.HappinessStatus + interval;
                if (!nextStatus.Equals(maxStatusValue))
                {
                    this.HappinessStatus = nextStatus;
                    this.LastModified = DateTime.UtcNow;
                }
            }
        }

        public void Decrease<T>(int interval)
        {
            var type = typeof(T);
            var maxStatusValue = Enum.GetValues(typeof(T)).Cast<T>().Min();
            if (type == typeof(HungrinessStatus))
            {
                var nextStatus = this.HungrinessStatus - interval;
                if (!nextStatus.Equals(maxStatusValue))
                {
                    this.HungrinessStatus = nextStatus;
                    this.LastModified = DateTime.UtcNow;
                }
            }
            else if (type == typeof(HappinessStatus))
            {
                var nextStatus = this.HappinessStatus - interval;
                if (!nextStatus.Equals(maxStatusValue))
                {
                    this.HappinessStatus = nextStatus;
                    this.LastModified = DateTime.UtcNow;
                }
            }
        }

        public void Stroke(int interval)
        {
            Increase<HappinessStatus>(interval);
        }

        public void Feed(int interval)
        {
            Decrease<HungrinessStatus>(interval);
        }

        public void SetPetType(PetType petType)
        {
            this.Type = petType;
        }

        public void SetUserId(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
