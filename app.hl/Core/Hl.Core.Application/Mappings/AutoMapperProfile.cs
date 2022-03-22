using AutoMapper;
using Hl.Core.Application.DTOs;
using Hl.Core.Domain.Models;
using CleanSolution.Core.Application.DTOs;
using Hl.Core.Application.Commons;

namespace Hl.Core.Application.Mappings
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
