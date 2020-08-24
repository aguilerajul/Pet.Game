using System;
using System.Collections.Generic;

namespace Pet.Game.API.Dtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Domain.Entities.Pet> Pets { get; set; }
    }
}
