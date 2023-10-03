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
        }
    }
}
