using AutoMapper;
using BulletinBoard.Contracts.User;
using UserEntity = BulletinBoard.Domain.User.User;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.User
{
    /// <summary>
    /// Маппер для <see cref="UserEntity"/>.
    /// </summary>
    public class UserMapper : Profile
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        public UserMapper()
        {
            CreateMap<CreateUserDto, UserEntity>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));

            CreateMap<UserEntity, UserDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Adverts, opt => opt.MapFrom(src => src.Adverts));

            CreateMap<UserEntity, InfoUserDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<UpdateUserDto, UserEntity>(MemberList.None)
                .IncludeBase<CreateUserDto, UserEntity>();
        }
    }
}
