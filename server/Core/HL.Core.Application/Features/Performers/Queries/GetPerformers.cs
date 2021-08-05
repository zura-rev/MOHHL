using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HL.Core.Application.Commons;
using HL.Core.Application.Interfaces;
using HL.Core.Domain.Models;
using System;
using HL.Core.Application.DTOs;
using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace HL.Core.Application.Features.Calls.Queries
{
    public class GetPerformerRequest : IRequest<GetPaginationDto<GetPerformerDto>>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public int Id { get; set; }
        public int CallId { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public DateTime PerformDate { get; set; }

    }

    public class GetPerformersHandler : IRequestHandler<GetPerformerRequest, GetPaginationDto<GetPerformerDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetPerformersHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetPerformerDto>> Handle(GetPerformerRequest request, CancellationToken cancellationToken)
        {
            var performers = unit.PerformerRepository.Filter(
                request.Id,
                request.CallId,
                request.UserId,
                request.UserType,
                request.Status,
                request.Note,
                request.PerformDate
            );

            var performerList = await Pagination<Performer>.CreateAsync(performers, request.pageIndex, request.pageSize);
            var result = mapper.Map<GetPaginationDto<GetPerformerDto>>(performerList);
            return result;
        }
    }

    public class GetPerformerValidator : AbstractValidator<GetPerformerRequest>
    {
        public GetPerformerValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
