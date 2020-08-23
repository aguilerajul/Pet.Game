using System;
using System.Collections.Generic;

namespace Pet.Game.API.Dtos
{
    public class UserRequestDto
    {
        public string Name { get; set; }
        public IEnumerable<Guid> PetIds { get; set; }
    }
}
