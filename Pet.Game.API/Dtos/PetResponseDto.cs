using System;
using Pet.Game.Domain.Entities;
using Pet.Game.Domain.Enums;

namespace Pet.Game.API.Dtos
{
    public class PetResponseDto
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public User User { get; set; }
        public HappinessStatus HappinessStatus { get; internal set; }
        public HungrinessStatus HungrinessStatus { get; internal set; }
    }
}
