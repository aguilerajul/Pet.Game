using System;
using Pet.Game.Domain.Entities;

namespace Pet.Game.API.Dtos
{
    public class PetResponseDto
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HungrinessStatus { get; set; }
        public string HappinessStatus { get; set; }
        public int HappinessDecreaseInterval { get; set; }
        public int HungrinessIncreaseInterval { get; set; }
        public PetType Type { get; set; }
    }
}
