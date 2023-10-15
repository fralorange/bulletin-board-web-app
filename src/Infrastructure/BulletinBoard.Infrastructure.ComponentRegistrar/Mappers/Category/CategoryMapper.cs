using AutoMapper;
using BulletinBoard.Contracts.Category;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Mappers.Category
{
    /// <summary>
    /// Маппер для <see cref="Domain.Category.Category"/>
    /// </summary>
    public class CategoryMapper : Profile
    {
        /// <summary>
        /// Маппер.
        /// </summary>
        public CategoryMapper()
        {
            CreateMap<Domain.Category.Category, CategoryDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Adverts, opt => opt.MapFrom(src => src.Adverts));

            CreateMap<Domain.Category.Category, InfoCategoryDto>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<CreateCategoryDto, Domain.Category.Category>(MemberList.None)
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId));

            CreateMap<UpdateCategoryDto, Domain.Category.Category>(MemberList.None)
                .IncludeBase<CreateCategoryDto, Domain.Category.Category>();
        }
    }
}
