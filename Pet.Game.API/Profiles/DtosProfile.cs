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

            CreateMap<Domain.Entities.Pet, Dtos.PetResponseDto>()
                .ForMember(dest => dest.HappinessStatus,
                           opt => opt.MapFrom(src => src.HappinessStatus.ToString()))
                .ForMember(dest => dest.HungrinessStatus,
                           opt => opt.MapFrom(src => src.HungrinessStatus.ToString()));
            CreateMap<Dtos.PetRequestDto, Domain.Entities.Pet>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.PetId));

        }
    }
}
