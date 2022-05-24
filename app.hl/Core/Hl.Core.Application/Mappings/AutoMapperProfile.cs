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
                //.ForMember(dest => dest.Card, opt => opt.MapFrom(src => src.Card != null ? src.Card : new Card()));
            CreateMap<Card, GetCardDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<User,GetUserDto>();
            CreateMap<User, GetOperatorDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src=>$"{src.FirstName} {src.LastName}"));
            CreateMap(typeof(Pagination<>), typeof(GetPaginationDto<>));
        }
    }
}
