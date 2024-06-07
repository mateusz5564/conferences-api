using AutoMapper;
using Conferences.Domain.Entities;

namespace Conferences.Application.Categories.Dtos
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
