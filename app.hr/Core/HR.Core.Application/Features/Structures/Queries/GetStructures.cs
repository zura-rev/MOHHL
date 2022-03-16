using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using FluentValidation;
using HR.Core.Application.Commons;
using HR.Core.Application.DTOs;
using HR.Core.Application.Interfaces;
using HR.Core.Domain.Enums;
using HR.Core.Domain.Models;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Core.Application.Features.Employees.Queries
{
    public class GetStructureRequest : IRequest<GetPaginationDto<GetStructureDto>>
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int PositionId { get; set; }
        public double DefaultSalary { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetStructureHandler : IRequestHandler<GetStructureRequest, GetPaginationDto<GetStructureDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public GetStructureHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetStructureDto>> Handle(GetStructureRequest request, CancellationToken cancellationToken)
        {
            var result = unit.StructureRepository.Filter(
                request.Id,
                request.SectionId,
                request.PositionId,
                request.DefaultSalary,
                request.Count
            );

            var structures = await Pagination<Structure>.CreateAsync(result, request.PageIndex, request.PageSize);
            return mapper.Map<GetPaginationDto<GetStructureDto>>(structures);
        }
    }

    public class GetStructureValidator : AbstractValidator<GetStructureRequest>
    {
        public GetStructureValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }

}
