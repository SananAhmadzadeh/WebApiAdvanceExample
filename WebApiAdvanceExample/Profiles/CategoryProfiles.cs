using AutoMapper;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.CategoryDTOs;

namespace WebApiAdvanceExample.Profiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<UpdateCategoryDto, Category>()
                     .ForMember(dest => dest.UpdatedAt,
                     opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CreateCategoryDto, Category>()
                    .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(_ => DateTime.UtcNow))
                    .ForMember(dest => dest.UpdatedAt,
                           opt => opt.MapFrom(_ => DateTime.UtcNow)).ReverseMap();

            CreateMap<Category, GetCategoryDto>();
        }
    }
}
