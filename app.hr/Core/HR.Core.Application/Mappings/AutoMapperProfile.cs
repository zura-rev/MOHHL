﻿using AutoMapper;
using HR.Core.Application.DTOs;
using HR.Core.Domain.Models;
using HR.Core.Domain.Enums;
using System;
using HR.Core.Application.Features.Positions.Commands;
using CleanSolution.Core.Application.DTOs;
using HR.Core.Application.Commons;
using HR.Core.Application.Features.Sections.Commands;

namespace HR.Core.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(GetPaginationDto<>));
            CreateMap<CreatePosition, Position>();
            CreateMap<SetPositionDto, Position>();
            CreateMap<Position, GetPositionDto>();
            CreateMap<Section, GetSectionDto>();
            CreateMap<UpsertSectionRequest, Section>();
            CreateMap<SetEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == Gender.Male ? "კაცი" : "ქალი"));
                //.ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year));

        }
    }
}
