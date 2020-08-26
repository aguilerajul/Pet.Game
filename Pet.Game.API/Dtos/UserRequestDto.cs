using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.API.Dtos
{
    public class UserRequestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
