using System;

namespace Pet.Game.API.Dtos
{
    public class PetRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HappinessDecreaseInterval { get; set; }
        public int HungrinessIncreaseInterval { get; set; }
        public Guid TypeId { get; set; }
    }
}
