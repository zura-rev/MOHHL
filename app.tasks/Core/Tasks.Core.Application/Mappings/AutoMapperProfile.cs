using AutoMapper;
using Tasks.Core.Application.DTOs;
using Tasks.Core.Domain.Models;
using CleanSolution.Core.Application.DTOs;
using Tasks.Core.Application.Commons;

namespace Tasks.Core.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Call, GetCallDto>();
            CreateMap<Card, GetCardDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<User,GetUserDto>();
            CreateMap(typeof(Pagination<>), typeof(GetPaginationDto<>));
        }
    }
}
