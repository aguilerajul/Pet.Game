using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.API.Dtos
{
    public class PetRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PetId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public Guid TypeId { get; set; }
    }
}
