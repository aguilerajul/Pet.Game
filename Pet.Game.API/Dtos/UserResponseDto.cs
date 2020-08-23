using System.Collections.Generic;

namespace Pet.Game.API.Dtos
{
    public class UserResponseDto
    {
        public string Name { get; set; }
        public IEnumerable<Domain.Entities.Pet> Pets { get; set; }
    }
}
