using System;
using System.ComponentModel.DataAnnotations;

namespace Pet.Game.API.Dtos
{
    public class PetTypeDto
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Range(1, Int32.MaxValue)]
        public int HappinessInterval { get; set; }

        [Range(1, Int32.MaxValue)]
        public int HungrinessInterval { get; set; }
    }
}
