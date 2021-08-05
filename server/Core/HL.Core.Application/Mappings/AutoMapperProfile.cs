using AutoMapper;
using HL.Core.Application.DTOs;
using HL.Core.Domain.Models;
using HL.Core.Domain.Enums;
using System;
using HL.Core.Application.Features.Positions.Commands;
using CleanSolution.Core.Application.DTOs;
using HL.Core.Application.Commons;

namespace HL.Core.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Call, GetCallDto>();
            CreateMap<Performer, GetPerformerDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<User,GetUserDto>();
            CreateMap(typeof(Pagination<>), typeof(GetPaginationDto<>));
            CreateMap<CreatePositionRequest, Position>();
            CreateMap<SetPositionDto, Position>();
            CreateMap<Position, GetPositionDto>();

            CreateMap<SetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == Gender.Male ? "კაცი" : "ქალი"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year));

        }
    }
}
