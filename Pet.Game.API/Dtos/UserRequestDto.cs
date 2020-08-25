using System.ComponentModel.DataAnnotations;

namespace Pet.Game.API.Dtos
{
    public class UserRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
