using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using FluentValidation;
using HR.Core.Application.Commons;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Queries
{
    public class GetSectionRequest : IRequest<GetPaginationDto<GetSectionDto>>
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public int ParentId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetSectionHandler : IRequestHandler<GetSectionRequest, GetPaginationDto<GetSectionDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetSectionHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetSectionDto>> Handle(GetSectionRequest request, CancellationToken cancellationToken)
        {
            var result = unit.SectionRepository.Filter(
                request.Id,
                request.SectionName,
                request.ParentId
            );

            var sections = await Pagination<Section>.CreateAsync(result, request.PageIndex, request.PageSize);
            return mapper.Map<GetPaginationDto<GetSectionDto>>(sections);
        }

    }

    public class GetSectionValidator : AbstractValidator<GetSectionRequest>
    {
        public GetSectionValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
