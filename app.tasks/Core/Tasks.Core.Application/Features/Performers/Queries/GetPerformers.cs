using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Core.Application.Commons;
using Tasks.Core.Application.Interfaces;
using Tasks.Core.Domain.Models;
using System;
using Tasks.Core.Application.DTOs;
using AutoMapper;
using CleanSolution.Core.Application.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace Tasks.Core.Application.Features.Calls.Queries
{
    public class GetCardRequest : IRequest<GetPaginationDto<GetCardDto>>
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

    public class GetCardHandler : IRequestHandler<GetCardRequest, GetPaginationDto<GetCardDto>>
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        public GetCardHandler(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        public async Task<GetPaginationDto<GetCardDto>> Handle(GetCardRequest request, CancellationToken cancellationToken)
        {
            var cards = unit.CardRepository.Filter(
                request.Id,
                request.CallId,
                request.UserId,
                request.UserType,
                request.Status,
                request.Note,
                request.PerformDate
            );

            var performerList = await Pagination<Card>.CreateAsync(cards, request.pageIndex, request.pageSize);
            var result = mapper.Map<GetPaginationDto<GetCardDto>>(performerList);
            return result;
        }
    }

    public class GetCardValidator : AbstractValidator<GetCardRequest>
    {
        public GetCardValidator()
        {
            RuleFor(x => x.pageIndex).GreaterThanOrEqualTo(1).WithMessage("მიუთითეთ გვერდის ნომერი");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("მიუთითეთ გვერდის ზომა");
        }
    }
}
