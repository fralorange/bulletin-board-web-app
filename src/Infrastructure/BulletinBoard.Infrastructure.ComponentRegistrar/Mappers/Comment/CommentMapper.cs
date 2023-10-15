using AutoMapper;
using BulletinBoard.Contracts.Comment;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Comment
{
    /// <summary>
    /// Маппер для <see cref="Domain.Comment.Comment"/>
    /// </summary>
    public class CommentMapper : Profile
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        public CommentMapper()
        {
            CreateMap<Domain.Comment.Comment, CommentDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Ad, opt => opt.MapFrom(src => src.Ad))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt));

            CreateMap<Domain.Comment.Comment, InfoCommentDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt))
                .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.AdId));

            CreateMap<CreateCommentDto, Domain.Comment.Comment>(MemberList.None)
                .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.AdId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));

            CreateMap<UpdateCommentDto, Domain.Comment.Comment>(MemberList.None)
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
        }
    }
}
