namespace BlogEngine.Domain.Profiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Entities.User, UserDto>().ReverseMap();

        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
