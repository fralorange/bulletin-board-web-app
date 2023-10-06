using AutoMapper;
using BulletinBoard.Contracts.Ad;

using AdEntity = BulletinBoard.Domain.Ad.Ad;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Ad
{
    /// <summary>
    /// Маппер для <see cref="AdEntity"/>.
    /// </summary>
    public class AdMapper : Profile
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        public AdMapper()
        {
            CreateMap<AdEntity, AdDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
                //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                //.ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))

            CreateMap<AdEntity, InfoAdDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<CreateAdDto, AdEntity>(MemberList.None)
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
                //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                //.ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.Select()))

            CreateMap<UpdateAdDto, AdEntity>(MemberList.None)
                .IncludeBase<CreateAdDto, AdEntity>();
        }
    }
}
