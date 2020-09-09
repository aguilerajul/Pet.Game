using AutoMapper;

namespace Pet.Game.API.Profiles
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            CreateMap<Domain.Entities.User, Dtos.UserResponseDto>();
            CreateMap<Dtos.UserRequestDto, Domain.Entities.User>();

            CreateMap<Domain.Entities.PetType, Dtos.PetTypeDto>();
            CreateMap<Dtos.PetTypeDto, Domain.Entities.PetType>();

            CreateMap<Domain.Entities.Pet, Dtos.PetResponseDto>();
            CreateMap<Dtos.PetRequestDto, Domain.Entities.Pet>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.PetId));

        }
    }
}
