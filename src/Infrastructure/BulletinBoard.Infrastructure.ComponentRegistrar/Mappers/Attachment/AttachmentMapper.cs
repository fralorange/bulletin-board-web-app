using AutoMapper;
using BulletinBoard.Application.AppServices.FileProcessing.Helpers;
using BulletinBoard.Contracts.Attachment;

using AttachmentEntity = BulletinBoard.Domain.Attachment.Attachment;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Attachment
{
    /// <summary>
    /// Маппер для <see cref="AttachmentEntity"/>
    /// </summary>
    public class AttachmentMapper : Profile
    {
        /// <summary>
        /// Маппер
        /// </summary>
        public AttachmentMapper()
        {
            CreateMap<AttachmentEntity, AttachmentDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Ad, opt => opt.MapFrom(src => src.Ad));

            CreateMap<CreateAttachmentDto, AttachmentEntity>(MemberList.None)
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => FileToBytesHelper.ProcessAsync(src.File).Result))
                .ForMember(dest => dest.AdId, opt => opt.MapFrom(src => src.AdId));
        }
    }
}
